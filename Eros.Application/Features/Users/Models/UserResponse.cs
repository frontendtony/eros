namespace Eros.Application.Features.Users.Models;

public record UserResponse(
    string Id,
    string Email,
    string FirstName,
    string LastName,
    string? Avatar
);