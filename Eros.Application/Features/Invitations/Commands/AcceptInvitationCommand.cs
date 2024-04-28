using Eros.Api.Dto.Invitations;
using Eros.Application.Abstractions;
using Eros.Domain.Aggregates.Invitations;

namespace Eros.Application.Features.Invitations.Commands;

public record AcceptInvitationCommand(
    string Code,
    InvitationStatus Status,
    Guid LoggedInUserId,
    string LoggedInUserEmail)
    : ICommand<AcceptInvitationCommandDto>;
