namespace Eros.Api.Dto.Invitations;

public record GetInvitationQueryDto
{
    public bool IsExistingUser { get; init; }
    public string RoleName { get; set; }
    public string EstateName { get; set; }
    public string Status { get; init; }
}
