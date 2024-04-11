using Eros.Api.Dto.Auth;
using Eros.Application.Abstractions;

namespace Eros.Application.Features.Auth.Commands;

public sealed record SignupCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName
) : ICommand<SignupCommandDto>;
