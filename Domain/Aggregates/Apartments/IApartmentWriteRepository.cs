namespace Eros.Domain.Aggregates.Apartments;

public interface IApartmentWriteRepository
{
    Task AddAsync(Apartment apartment, CancellationToken cancellationToken = default);
    Task UpdateAsync(Apartment apartment, CancellationToken cancellationToken = default);
    Task DeleteAsync(Apartment apartment, CancellationToken cancellationToken = default);
}
