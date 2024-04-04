namespace Eros.Domain.Aggregates.Roles;

public class RolePermission
{
    public Guid RoleId { get; init; }
    public Guid PermissionId { get; init; }
}