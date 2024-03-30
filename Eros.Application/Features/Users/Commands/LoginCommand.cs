using Application.Features.Users.Models;
using MediatR;

namespace Eros.Application.Features.Users.Commands;

public class LoginCommand : IRequest<LoginResponse>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
