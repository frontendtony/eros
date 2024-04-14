using Eros.Domain.Aggregates.Roles;
using Eros.Domain.Aggregates.Users;
using Eros.Domain.Enums;

namespace Eros.Domain.Aggregates.Estates;

public class EstateUser
{
    public required Guid EstateId { get; init; }
    public required Guid UserId { get; init; }
    public required Guid RoleId { get; init; }
    public required Guid CreatedBy { get; init; }
    public Guid? ApartmentId { get; init; }

    public Role Role { get; init; } = null!;
    public Estate Estate { get; init; } = null!;
    public User User { get; init; } = null!;

    public EstateUserType EstateUserType { get; init; } = EstateUserType.Resident;
    public bool IsDeleted { get; private set; }

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
}