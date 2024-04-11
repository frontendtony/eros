using Eros.Domain.Aggregates.Apartments;
using Eros.Domain.Aggregates.Estates;

namespace Eros.Domain.Aggregates.Buildings;

public class Building
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public string? Description { get; set; }
    public required Guid EstateId { get; set; }
    public Estate Estate { get; private set; } = null!;
    public required Guid BuildingTypeId { get; set; }
    public BuildingType? BuildingType { get; private set; }
    public List<Apartment> Apartments { get; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public void Update(string name, string description, string address, Guid buildingTypeId)
    {
        Name = name ?? Name;
        Description = description ?? Description;
        Address = address ?? Address;
        BuildingTypeId = Guid.TryParse(buildingTypeId.ToString(), out var id) ? id : BuildingTypeId;

        UpdatedAt = DateTime.UtcNow;
    }
}
