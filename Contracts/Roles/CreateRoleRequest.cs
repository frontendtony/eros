namespace Eros.Contracts.Roles;

public record CreateRoleRequest(
    string Name,
    string Description,
    string[]? PermissionIds
);
