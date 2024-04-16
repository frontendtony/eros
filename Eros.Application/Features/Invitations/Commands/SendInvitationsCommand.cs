namespace Eros.Application.Features.Invitations.Commands;

public sealed record SendInvitationsCommand
{
    public required string[] Emails { get; init; }
    public required Guid RoleId { get; init; }
    public required Guid EstateId { get; init; }
    public required Guid SenderId { get; init; }
}