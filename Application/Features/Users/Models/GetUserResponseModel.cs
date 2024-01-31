
using Eros.Domain.Aggregates.Users;

namespace Eros.Application.Features.Users.Models;

public class GetUserResponseModel
{
    public GetUserResponseModel(User user)
    {
        Id = user.Id;
        Email = user.Email;
        FirstName = user.FirstName;
        LastName = user.LastName;
        PhoneNumber = user.PhoneNumber;
        Avatar = user.Avatar;
        CreatedAt = user.CreatedAt;
        UpdatedAt = user.UpdatedAt;
    }

    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public string? Avatar { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
