using Eros.Application.Features.Users.Models;
using MediatR;

namespace Eros.Application.Features.Users.Queries;

public record GetUserQuery(Guid UserId) : IRequest<UserResponse>;