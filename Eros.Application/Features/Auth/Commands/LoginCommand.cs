using Eros.Api.Dto.Auth;
using MediatR;

namespace Eros.Application.Features.Auth.Commands;

public class LoginCommand : IRequest<LoginCommandDto>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
