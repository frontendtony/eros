using Eros.Api.Dto.Invitations;
using Eros.Application.Exceptions;
using Eros.Application.Features.Invitations.Commands;
using Eros.Domain.Aggregates.Estates;
using Eros.Domain.Aggregates.Invitations;
using Eros.Persistence;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Eros.Application.Features.Invitations.CommandHandlers;

public class AcceptInvitationCommandHandler(
    ErosDbContext dbContext,
    IInvitationReadRepository invitationReadRepository,
    IInvitationWriteRepository invitationWriteRepository,
    IEstateUserReadRepository estateUserReadRepository,
    IEstateUserWriteRepository estateUserWriteRepository,
    ILogger<AcceptInvitationCommandHandler> logger
) : IRequestHandler<AcceptInvitationCommand, AcceptInvitationCommandDto>
{
    public async Task<AcceptInvitationCommandDto> Handle(AcceptInvitationCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Accepting invitation {Code}", command.Code);
        var invitation = await invitationReadRepository.GetByCodeAsync(command.Code, cancellationToken)
                         ?? throw new NotFoundException("Invitation not found");

        if (invitation.Status != InvitationStatus.Pending)
        {
            logger.LogError("Invitation has been processed previously. {Code}", command.Code);
            throw new InvalidOperationException("Invitation has been processed previously");
        }

        // check that the invitation is not expired
        if (invitation.IsExpired)
        {
            logger.LogError("Invitation has expired. {Code}", command.Code);
            throw new InvalidOperationException("Invitation has expired");
        }

        if (invitation.Email != command.LoggedInUserEmail)
        {
            logger.LogError("Invitation does not belong to the user. {Code} {Email}", command.Code,
                command.LoggedInUserEmail);
            throw new InvalidOperationException("Invitation does not belong to the user");
        }

        var estateUser = await estateUserReadRepository.GetByEstateIdAndUserIdAsync(
            invitation.EstateId, command.LoggedInUserId, cancellationToken);

        if (estateUser != null)
        {
            logger.LogError("User is already a member of the estate. {EstateId} {UserId}", invitation.EstateId,
                command.LoggedInUserId);
            throw new InvalidOperationException("User is already a member of the estate");
        }

        var newEstateUser = new EstateUser
        {
            EstateId = invitation.EstateId,
            UserId = command.LoggedInUserId,
            RoleId = invitation.RoleId,
            CreatedBy = command.LoggedInUserId
        };

        invitation.Accept(command.LoggedInUserId);

        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await estateUserWriteRepository.AddAsync(newEstateUser, cancellationToken);
            invitationWriteRepository.UpdateAsync(invitation, cancellationToken);

            logger.LogInformation("Saving changes to the database");
            await dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            logger.LogInformation("Invitation accepted. {Code}", command.Code);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error accepting invitation. {Code}", command.Code);
            await transaction.RollbackAsync(cancellationToken);

            throw new DatabaseException("Error accepting invitation", ex);
        }

        return invitation.Adapt<AcceptInvitationCommandDto>();
    }
}
