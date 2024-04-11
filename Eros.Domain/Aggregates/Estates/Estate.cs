using Eros.Domain.Aggregates.Buildings;
using Eros.Domain.Aggregates.Roles;
using Eros.Domain.Aggregates.Users;

namespace Eros.Domain.Aggregates.Estates;

public class Estate
{
    public Estate(
        string name,
        string address,
        Guid createdBy,
        string? latlng = default
    )
    {
        Id = Guid.NewGuid();
        Name = name;
        Address = address;
        CreatedBy = createdBy;
        LatLng = latlng;
        CreatedAt = DateTime.UtcNow;
    }
    
    public Estate() {}

    public Guid Id { get; init; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public Guid CreatedBy { get; init; }
    public Guid OwnerId { get; private set; } // Estate ownership can be transferred to another user
    public string? LatLng { get; set; }
    public List<Building> Buildings { get; } = [];
    public List<Role> Roles { get; } = [];
    public List<User> Users { get; } = [];
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; }
}
