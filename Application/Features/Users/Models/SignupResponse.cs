namespace Eros.Application.Features.Users.Models;

public class SignupCommandResponse
{
    public SignupCommandResponse(string token, string email, string firstName, string lastName, string? avatar)
    {
        Token = token;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Avatar = avatar;
    }

    public string Token { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Avatar { get; set; }
}
