namespace Eros.Api.Dto.Auth;

public class SignupCommandDto
{
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? Token { get; set; }
}