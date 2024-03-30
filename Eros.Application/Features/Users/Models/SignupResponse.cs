namespace Eros.Application.Features.Users.Models;

public class SignupCommandResponse(string token, string email, string firstName, string lastName, string? avatar)
{
    public string Token { get; init; } = token;
    public string Email { get; init; } = email;
    public string FirstName { get; init; } = firstName;
    public string LastName { get; init; } = lastName;
    public string? Avatar { get; init; } = avatar;
}
