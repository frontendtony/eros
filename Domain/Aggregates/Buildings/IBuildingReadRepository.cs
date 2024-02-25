using Eros.Domain.Aggregates.Apartments;

namespace Eros.Domain.Aggregates.Buildings;

public interface IBuildingReadRepository
{
    Task<Building?> GetByIdAsync(Guid buildingId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Apartment>> GetApartmentsAsync(Guid buildingId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Building>> GetByEstateIdAsync(Guid estateId, CancellationToken cancellationToken = default);
}
