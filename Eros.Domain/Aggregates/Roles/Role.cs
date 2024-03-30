using Eros.Domain.Aggregates.Estates;

namespace Eros.Domain.Aggregates.Roles;

public class Role
{
    private Role(string name, string description, bool isShared = false)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        IsShared = isShared;
        CreatedAt = DateTime.UtcNow;
    }

    public static Role CreateEstateRole(string name, string description)
    {
        return new Role(name, description);
    }

    public static Role CreateSharedRole(string name, string description)
    {
        return new Role(name, description, true);
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<Permission> Permissions { get; set; } = new List<Permission>();
    public IEnumerable<Estate> Estates { get; set; } = new List<Estate>();
    public bool IsShared { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
}
