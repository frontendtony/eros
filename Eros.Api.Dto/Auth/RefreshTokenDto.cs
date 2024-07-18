namespace Eros.Api.Dto.Auth;

public sealed record RefreshTokenDto(string RefreshToken, string ClientId, string ClientSecret, string GrantType = "refresh_token");
