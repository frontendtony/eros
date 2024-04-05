namespace Eros.Api.Dto.Roles;

public sealed record CreateSharedRoleDto(
    string Name,
    string Description,
    List<string> PermissionIds
);