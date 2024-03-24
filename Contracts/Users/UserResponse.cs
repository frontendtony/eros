namespace Eros.Contracts.Users;

public record UserResponse(
    string Id,
    string Email,
    string FirstName,
    string LastName,
    string? Avatar
);