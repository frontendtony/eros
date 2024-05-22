namespace Eros.Api.Dto.Invitations;

public record GetInvitationQueryDto
{
  public bool IsExistingUser { get; init; }
  public string RoleName { get; set; } = string.Empty;
  public string EstateName { get; set; } = string.Empty;
  public string Email { get; init; }
  public required string Status { get; init; }
  public string Expiration { get; init; }
  public string InviterName { get; set; }
  public bool IsExpired { get; init; }
}
