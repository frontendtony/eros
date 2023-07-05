namespace EstateManager.Models;

// public record UserResponseModel
// (
//     Guid Id,
//     string Email,
//     string FirstName,
//     string LastName,
//     string? PhoneNumber,
//     string? Avatar
// );
public class UserResponseModel
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public string? Avatar { get; set; } = string.Empty;
}
