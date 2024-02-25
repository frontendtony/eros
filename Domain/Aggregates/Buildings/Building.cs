using Eros.Domain.Aggregates.Apartments;
using Eros.Domain.Aggregates.Estates;

namespace Eros.Domain.Aggregates.Buildings;

public class Building
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; private set; }
    public required Guid EstateId { get; set; }
    public Estate? Estate { get; private set; }
    public required Guid BuildingTypeId { get; set; }
    public BuildingType? BuildingType { get; private set; }
    public ICollection<Apartment>? Apartments { get; set; }
    public DateTime CreatedAt { get; set; }
}
