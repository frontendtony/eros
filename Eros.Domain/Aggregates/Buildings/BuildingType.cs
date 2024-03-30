namespace Eros.Domain.Aggregates.Buildings;

public class BuildingType
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}
