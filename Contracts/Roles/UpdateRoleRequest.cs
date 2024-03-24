namespace Eros.Contracts.Roles;

public record UpdateRoleRequest(
    string Name,
    string Description,
    string[]? PermissionIds
);
