using Eros.Domain.Aggregates.Users;

namespace Eros.Api.Dto.Auth;

public record LoginCommandDto(
    string Token,
    DateTime ExpiresAt,
    UserDto User
);