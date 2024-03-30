namespace Eros.Api.Contracts.Users;

public record SignupRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string? Avatar
);
