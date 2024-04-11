using Eros.Domain.Aggregates.Roles;
using Eros.Domain.Aggregates.Users;
using Eros.Domain.Enums;

namespace Eros.Domain.Aggregates.Estates;

public class EstateUser
{
    public Guid EstateId { get; init; }
    public Guid UserId { get; init; }
    public Guid RoleId { get; init; }
    public Guid? ApartmentId { get; init; }

    public Role Role { get; init; } = null!;
    public Estate Estate { get; init; } = null!;
    public User User { get; init; } = null!;
    
    public EstateUserType EstateUserType { get; init; }
    public bool IsDeleted { get; private set; }
    
    public Guid CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? DeletedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
}