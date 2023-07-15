namespace EstateManager.Entities;

public class EstateRole
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IEnumerable<EstateRolePermission> Permissions { get; set; } = new List<EstateRolePermission>();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
