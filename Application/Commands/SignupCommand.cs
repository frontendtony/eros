using System.ComponentModel.DataAnnotations;

namespace EstateManager.Commands;

public class SignupCommand
{
    public required string Email;
    public required string Password;
    public required string FirstName;
    public required string LastName;
    public string? PhoneNumber;
    public string? Avatar;
}
