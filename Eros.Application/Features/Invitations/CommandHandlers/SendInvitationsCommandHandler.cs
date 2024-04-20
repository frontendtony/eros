using Eros.Application.Exceptions;
using Eros.Application.Features.Invitations.Commands;
using Eros.Domain.Aggregates.Estates;
using Eros.Domain.Aggregates.Invitations;
using Eros.Domain.Aggregates.Roles;
using Eros.Domain.Aggregates.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Eros.Application.Features.Invitations.CommandHandlers;

public class SendInvitationsCommandHandler(
    IRoleReadRepository _roleReadRepository,
    IEstateUserReadRepository _estateUserReadRepository,
    IUserReadRepository _userReadRepository,
    IInvitationReadRepository _invitationReadRepository,
    ILogger<SendInvitationsCommandHandler> _logger
) : IRequestHandler<SendInvitationsCommand, bool>
{
    public async Task<bool> Handle(SendInvitationsCommand request, CancellationToken cancellationToken)
    {
        var estateUser = await _estateUserReadRepository.GetByEstateIdAndUserIdAsync(request.EstateId, request.SenderId, cancellationToken);

        if (estateUser == null)
        {
            _logger.LogError("User is not a member of the estate. {EstateId} {SenderId}", request.EstateId, request.SenderId);
            throw new BadRequestException("You cannot send invitations to an estate you are not a member of.");
        }

        var role = await _roleReadRepository.GetByIdAsync(request.RoleId);

        if (role == null)
        {
            _logger.LogError("The role does not exist. {RoleId}", request.RoleId);
            throw new BadRequestException("The role does not exist.");
        }

        var rolePermisionIds = role.Permissions.Select(p => p.Id);

        var hasPermissions = await _roleReadRepository.HasPermissionsAsync(estateUser.RoleId, rolePermisionIds, cancellationToken);

        if (!hasPermissions)
        {
            // User can only assign roles with permissions that he has
            _logger.LogError("User does not have the required permissions to assign the role. {EstateId} {SenderId} {RoleId}", request.EstateId, request.SenderId, request.RoleId);
            throw new BadRequestException("You can only assign roles with permissions that you have.");
        }

        List<Invitation> invitations = [];

        foreach (var email in request.Emails)
        {
            // check if the user already exists
            var user = await _userReadRepository.GetByEmailAsync(email);
            if (user != null)
            {
                // check if the user is an eros admin
                if (user.IsAdmin)
                {
                    _logger.LogWarning("User is an Eros admin. {UserId}", user.Id);
                    continue;
                }

                // check if the user is already a member of the estate
                var existingEstateUser = await _estateUserReadRepository.GetByEstateIdAndUserIdAsync(request.EstateId, user.Id, cancellationToken);
                if (existingEstateUser != null)
                {
                    _logger.LogWarning("User is already a member of the estate. {EstateId} {UserId}", request.EstateId, user.Id);
                    continue;
                }
            }

            // check if the user has an existing invitation for the estate with the same email
            var existingInvitation = await _invitationReadRepository.GetByEstateIdAndEmailAsync(request.EstateId, email, cancellationToken);
            if (existingInvitation != null)
            {
                _logger.LogWarning("User has an existing invitation for the estate. {EstateId} {Email}", request.EstateId, email);

                // check if the role is different
                if (existingInvitation.RoleId != request.RoleId)
                {
                    _logger.LogWarning("User has an existing invitation for the estate with a different role. {EstateId} {Email} {RoleId} {ExistingRoleId}", request.EstateId, email, request.RoleId, existingInvitation.RoleId);
                    existingInvitation.Cancel();
                }
                // extend the expiration date if the invitation is pending
                if (existingInvitation.Status == InvitationStatus.Pending)
                {
                    existingInvitation.ResetExpiration();
                    invitations.Add(existingInvitation);
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

            invitations.Add(invitation);
        }

        await SendInvitationEmails(invitations);

        return true;
    }

    private Task SendInvitationEmails(List<Invitation> invitations)
    {
        _logger.LogInformation("Sending invitation emails...");

        foreach (var invitation in invitations)
        {
            _logger.LogInformation("Invitation: {Email} {Id}", invitation.Email, invitation.Id);
        }
        return Task.CompletedTask;
    }
}