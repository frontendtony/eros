namespace Application.Features.Users.Models;

public class LoginResponse(string token)
{
    public string Token { get; set; } = token;
}