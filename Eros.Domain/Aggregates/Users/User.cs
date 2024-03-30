using Microsoft.AspNetCore.Identity;

namespace Eros.Domain.Aggregates.Users;

public class User : IdentityUser
{
    private User(string email, string firstName, string lastName, string? avatar, bool isAdmin = false)
    {
        Email = email;
        UserName = email;
        FirstName = firstName;
        LastName = lastName;
        Avatar = avatar;
        IsAdmin = isAdmin;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public new string Email { get; set; }
    public new string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Avatar { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static User Create(string email, string firstName, string lastName, string? avatar)
    {
        return new User(email, firstName, lastName, avatar);
    }

    public static User CreateAdmin(string email, string firstName, string lastName, string? avatar)
    {
        return new User(email, firstName, lastName, avatar, true);
    }
}
