using Microsoft.AspNetCore.Identity;

namespace Eros.Domain.Aggregates.Users;

public class User : IdentityUser<Guid>
{
    private User(string email, string firstName, string lastName, bool isAdmin = false)
    {
        Email = email;
        UserName = email;
        FirstName = firstName;
        LastName = lastName;
        IsAdmin = isAdmin;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    public sealed override string? Email { get; set; }
    public sealed override string? UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Avatar { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public static User Create(string email, string firstName, string lastName)
    {
        return new User(email, firstName, lastName);
    }

    public static User CreateAdmin(string email, string firstName, string lastName, string? avatar)
    {
        return new User(email, firstName, lastName, true);
    }
}
