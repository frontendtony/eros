namespace Eros.Api.Dto.Invitations;

public record GetInvitationQueryDto
{
    public bool IsExistingUser { get; init; }
    public string RoleName { get; set; } = string.Empty;
    public string EstateName { get; set; } = string.Empty;
    public required string Status { get; init; }
}
