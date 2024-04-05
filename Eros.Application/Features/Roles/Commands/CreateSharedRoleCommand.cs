using Eros.Api.Dto.Roles;
using ErrorOr;
using MediatR;

namespace Eros.Application.Features.Roles.Commands;

public sealed record CreateSharedRoleCommand(
    string Name,
    string Description,
    List<Guid> PermissionIds
) : IRequest<ErrorOr<CreateSharedRoleCommandDto>>;