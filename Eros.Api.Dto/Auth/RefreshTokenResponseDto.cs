namespace Eros.Api.Dto.Auth;

public sealed record RefreshTokenResponseDto(string Token, DateTime ExpiresAt, string RefreshToken);
