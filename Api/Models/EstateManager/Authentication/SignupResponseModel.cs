using EstateManager.Entities;

namespace Api.ResponseModels;

public class SignupResponseModel
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public string? Avatar { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Token { get; set; }

    public SignupResponseModel(User user, string token)
    {
        Id = user.Id;
        Email = user.Email;
        FirstName = user.FirstName;
        LastName = user.LastName;
        PhoneNumber = user.PhoneNumber;
        Avatar = user.Avatar;
        CreatedAt = user.CreatedAt;
        UpdatedAt = user.UpdatedAt;
        Token = token;
    }
}