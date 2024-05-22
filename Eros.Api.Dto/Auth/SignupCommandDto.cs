namespace Eros.Api.Dto.Auth;

public record SignupCommandDto(
  string Token,
  DateTime ExpiresAt,
  UserDto User
);
