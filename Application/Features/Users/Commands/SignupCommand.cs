namespace Eros.Application.Features.Users.Commands;

public class SignupCommand
{
    public required string Email;
    public required string Password;
    public required string FirstName;
    public required string LastName;
    public string? Avatar;
}
