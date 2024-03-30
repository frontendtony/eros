namespace Eros.Application.Features.Users.Commands;

public class LoginCommand
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
