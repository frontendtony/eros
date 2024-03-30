using Eros.Domain.Aggregates.Buildings;

namespace Eros.Domain.Aggregates.Apartments;

public class Apartment
{
    public Guid Id { get; private set; }
    public required string Name { get; set; }
    public string? Description { get; private set; }
    public int NumberOfRooms { get; set; }
    public required Guid ApartmentTypeId { get; set; }
    public ApartmentType? ApartmentType { get; private set; }
    public required Guid BuildingId { get; set; }
    public Building? Building { get; private set; }
}
