namespace Eros.Application.Features.Users.Models;

public class SignupCommandResponse(string token, string email, string firstName, string lastName, string? avatar)
{
    public string Token { get; set; } = token;
    public string Email { get; set; } = email;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string? Avatar { get; set; } = avatar;
}
