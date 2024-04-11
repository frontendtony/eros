using Eros.Api.Dto.Auth;
using Eros.Application.Abstractions;

namespace Eros.Application.Features.Auth.Commands;

public sealed record LoginCommand(
    string Email,
    string Password
) : ICommand<LoginCommandDto>;