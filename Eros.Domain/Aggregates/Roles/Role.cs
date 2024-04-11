using System.Collections;
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

    public Guid Id { get; init; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public List<Permission> Permissions { get; set; } = [];
    public List<Estate> Estates { get; } = [];
    public bool IsShared { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; private set; }
}
