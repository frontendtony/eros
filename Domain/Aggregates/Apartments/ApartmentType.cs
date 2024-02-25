namespace Eros.Domain.Aggregates.Apartments;

public class ApartmentType
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}
