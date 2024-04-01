namespace Eros.Api.Dto.Auth;

public sealed record LoginDto(
    string Email,
    string Password
);