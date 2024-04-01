using Eros.Api.Dto.Auth;
using Eros.Application.Features.Users.Models;
using MediatR;

namespace Eros.Application.Features.Auth.Commands;

public sealed record SignupCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName
) : IRequest<SignupCommandDto>;
