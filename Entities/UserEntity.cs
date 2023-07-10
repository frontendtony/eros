namespace EstateManager.Entities;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public override string? Email { get; set; }
    public override string? UserName { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Avatar { get; set; }
    public bool IsAdmin { get; set; } = default;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
