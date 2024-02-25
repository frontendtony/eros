namespace Eros.Domain.Aggregates.Apartments;

public interface IApartmentReadRepository
{
    Task<Apartment?> GetByIdAsync(Guid apartmentId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Apartment>> GetByBuildingIdAsync(Guid buildingId, CancellationToken cancellationToken = default);
}