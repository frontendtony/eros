namespace Eros.Api.Contracts.Users;

public record LoginRequest(
    string Email,
    string Password
);
