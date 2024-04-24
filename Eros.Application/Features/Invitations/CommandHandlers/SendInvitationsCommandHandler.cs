#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

using Eros.Application.EmailService;
using Eros.Application.Exceptions;
using Eros.Application.Features.Invitations.Commands;
using Eros.Domain.Aggregates.Estates;
using Eros.Domain.Aggregates.Invitations;
using Eros.Domain.Aggregates.Roles;
using Eros.Domain.Aggregates.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Eros.Application.Features.Invitations.CommandHandlers;

public class SendInvitationsCommandHandler(
    DbContext dbContext,
    IRoleReadRepository roleReadRepository,
    IEstateUserReadRepository estateUserReadRepository,
    IUserReadRepository userReadRepository,
    IInvitationReadRepository invitationReadRepository,
    IEmailClient _emailClient,
    IInvitationWriteRepository invitationWriteRepository,
    ILogger<SendInvitationsCommandHandler> logger
) : IRequestHandler<SendInvitationsCommand, bool>
{
    public async Task<bool> Handle(SendInvitationsCommand request, CancellationToken cancellationToken)
    {
        var estateUser = await estateUserReadRepository.GetByEstateIdAndUserIdAsync(request.EstateId, request.SenderId, cancellationToken);

        if (estateUser == null)
        {
            logger.LogError("User is not a member of the estate. {EstateId} {SenderId}", request.EstateId, request.SenderId);
            throw new BadRequestException("You cannot send invitations to an estate you are not a member of.");
        }

        var role = await roleReadRepository.GetByIdAsync(request.RoleId);

        if (role == null)
        {
            logger.LogError("The role does not exist. {RoleId}", request.RoleId);
            throw new BadRequestException("The role does not exist.");
        }

        var rolePermissionIds = role.Permissions.Select(p => p.Id);

        var hasPermissions = await roleReadRepository.HasPermissionsAsync(estateUser.RoleId, rolePermissionIds, cancellationToken);

        if (!hasPermissions)
        {
            // User can only assign roles with permissions that he has
            logger.LogError("User does not have the required permissions to assign the role. {EstateId} {SenderId} {RoleId}", request.EstateId, request.SenderId, request.RoleId);
            throw new BadRequestException("You can only assign roles with permissions that you have.");
        }

        List<Invitation> invitationsToSend = [];

        foreach (var email in request.Emails)
        {
            // check if the user already exists
            var user = await userReadRepository.GetByEmailAsync(email);
            if (user != null)
            {
                // check if the user is an eros admin
                if (user.IsAdmin)
                {
                    logger.LogWarning("User is an Eros admin. {UserId}", user.Id);
                    continue;
                }

                // check if the user is already a member of the estate
                var existingEstateUser = await estateUserReadRepository.GetByEstateIdAndUserIdAsync(request.EstateId, user.Id, cancellationToken);
                if (existingEstateUser != null)
                {
                    logger.LogWarning("User is already a member of the estate. {EstateId} {UserId}", request.EstateId, user.Id);
                    continue;
                }
            }

            // check if the user has an existing invitation for the estate with the same email
            var existingInvitation = await invitationReadRepository.GetByEstateIdAndEmailAsync(request.EstateId, email, cancellationToken);
            if (existingInvitation != null)
            {
                logger.LogWarning("User has an existing invitation for the estate. {EstateId} {Email}", request.EstateId, email);

                // check if the role is different
                if (existingInvitation.RoleId != request.RoleId)
                {
                    logger.LogWarning("User has an existing invitation for the estate with a different role. {EstateId} {Email} {RoleId} {ExistingRoleId}", request.EstateId, email, request.RoleId, existingInvitation.RoleId);
                    existingInvitation.Cancel();
                }
                // extend the expiration date if the invitation is pending
                if (existingInvitation.Status == InvitationStatus.Pending)
                {
                    existingInvitation.ResetExpiration();

                    invitationsToSend.Add(existingInvitation);
                    invitationWriteRepository.UpdateAsync(existingInvitation, cancellationToken);

                    continue;
                }
            }

            // add a new invitation and set the user id if the user already exists
            var invitation = new Invitation
            {
                Email = email,
                RoleId = request.RoleId,
                EstateId = request.EstateId,
                CreatedBy = request.SenderId,
            };

            if (user != null)
            {
                invitation.MapUser(user.Id);
            }

            invitationsToSend.Add(invitation);
            await invitationWriteRepository.AddAsync(invitation, cancellationToken);
        }

        if (invitationsToSend.Count == 0)
        {
            logger.LogWarning("No invitations to send.");
            return false;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        Task.Run(() => SendInvitationEmails(invitationsToSend), cancellationToken);

        return true;
    }

    private Task SendInvitationEmails(List<Invitation> invitations)
    {
        logger.LogInformation("Sending invitation emails...");

        foreach (var invitation in invitations)
        {
            //fetch invitation email html content
            //send email
            //probably move this to a service that gets the email content
            //_emailClient.Send();
            logger.LogInformation("Invitation: {Email} {Id}", invitation.Email, invitation.Id);
        }
        return Task.CompletedTask;
    }
}