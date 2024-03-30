using Eros.Application.Features.Users.Models;
using MediatR;

namespace Eros.Application.Features.Users.Commands;

public sealed record SignupCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string? Avatar
) : IRequest<SignupCommandResponse>;
