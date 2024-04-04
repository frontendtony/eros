namespace Eros.Domain.Aggregates.Roles;

public class Permission(Guid id, string name)
{
    public Guid Id { get; init; } = id;
    public string Name { get; init; } = name;
    public IEnumerable<Role> Roles { get; } = new List<Role>();
}
