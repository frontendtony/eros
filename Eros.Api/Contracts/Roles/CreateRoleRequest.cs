namespace Eros.Api.Contracts.Roles;

public record CreateRoleRequest(
    string Name,
    string Description,
    string[]? PermissionIds
);
