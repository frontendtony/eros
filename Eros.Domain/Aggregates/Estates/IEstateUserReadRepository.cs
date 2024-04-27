namespace Eros.Domain.Aggregates.Estates;

public interface IEstateUserReadRepository
{
    Task<EstateUser?> GetByEstateIdAndUserIdAsync(Guid estateId, Guid userId, CancellationToken cancellationToken);
    Task<EstateUser[]> GetEstateUsersByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
