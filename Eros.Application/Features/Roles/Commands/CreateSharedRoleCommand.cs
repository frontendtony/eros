using Eros.Api.Dto.Roles;
using Eros.Application.Abstractions;

namespace Eros.Application.Features.Roles.Commands;

public sealed record CreateSharedRoleCommand(
    string Name,
    string Description,
    List<Guid> PermissionIds
) : ICommand<CreateSharedRoleCommandDto>;