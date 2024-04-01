using Eros.Api.Dto.Auth;
using MediatR;
using ErrorOr;

namespace Eros.Application.Features.Auth.Commands;

public sealed record LoginCommand(
    string Email,
    string Password
) : IRequest<ErrorOr<LoginCommandDto>>;