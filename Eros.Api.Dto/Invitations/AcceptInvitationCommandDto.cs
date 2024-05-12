namespace Eros.Api.Dto.Invitations;

public record AcceptInvitationCommandDto
{
    public required string Code { get; init; }
    public required string Status { get; init; }
    public required string UserId { get; init; }
    public required string EstateId { get; init; }
    public required string RoleId { get; init; }
}
