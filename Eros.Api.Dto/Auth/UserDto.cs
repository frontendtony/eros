namespace Eros.Api.Dto.Auth;

public sealed record UserDto
(
    string Id,
    string Email,
    string FirstName,
    string LastName,
    bool IsEmailVerified,
    bool IsAdmin
);
