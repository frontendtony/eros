namespace Eros.Domain.Aggregates.Apartments;

public class ApartmentType
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
}
