namespace EstateManager.Models;

public record UserResponseModel
(
    string Email,
    string FirstName,
    string LastName,
    string? PhoneNumber,
    string? Avatar
);
