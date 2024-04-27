namespace Eros.Api.Dto.Invitations;

public sealed record SendInvitationsDto
{
    public required string[] Emails { get; init; }
    public required string RoleId { get; init; }
    public required string EstateId { get; init; }
}