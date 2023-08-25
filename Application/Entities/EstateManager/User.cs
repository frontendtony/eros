namespace EstateManager.Entities;

using EstateManager.Commands;
using EstateManager.Models;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public User()
    {
    }

    public User(SignupCommand command)
    {
        Email = command.Email;
        UserName = command.Email;
        FirstName = command.FirstName;
        LastName = command.LastName;
        Avatar = command.Avatar;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public new string Email { get; set; } = string.Empty;
    public override string? UserName { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Avatar { get; set; }
    public bool IsAdmin { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
