namespace Eros.Domain.Aggregates.Roles;

public class Permission(string name, string description)
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; init; } = name;
    public required string Description { get; init; } = description;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
