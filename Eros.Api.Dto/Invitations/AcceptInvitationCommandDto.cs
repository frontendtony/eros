namespace Eros.Api.Dto.Invitations;

public record AcceptInvitationCommandDto
{
    public string Code { get; init; }
    public string Status { get; init; }
    public string UserId { get; init; }
    public string EstateId { get; init; }
    public string RoleId { get; init; }
}