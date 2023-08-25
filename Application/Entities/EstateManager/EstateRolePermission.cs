namespace EstateManager.Entities;

public class EstateRolePermission
{
    public Guid Id { get; set; }
    public Guid EstateRoleId { get; set; }
    public Guid EstatePermissionId { get; set; }
    public EstatePermission? EstatePermission { get; set; }
}
