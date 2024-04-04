using Eros.Domain.Aggregates.Roles;

namespace Eros.Domain.Aggregates.Estates;

public class EstateRole
{
    public Guid EstateId { get; init; }
    public Guid RoleId { get; init; }
    public Estate? Estate { get; init; }
    public Role? Role { get; init; }
}
