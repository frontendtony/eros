namespace Eros.Api.Dto.Auth;

public sealed record SignupDto(
    string Email,
    string Password,
    string FirstName,
    string LastName
);