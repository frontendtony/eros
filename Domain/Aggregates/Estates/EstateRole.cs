using Eros.Domain.Aggregates.Roles;

namespace Eros.Domain.Aggregates.Estates;

public class EstateRole
{
    public Guid EstateId { get; set; }
    public Guid RoleId { get; set; }
    public Estate? Estate { get; set; }
    public Role? Role { get; set; }
}
