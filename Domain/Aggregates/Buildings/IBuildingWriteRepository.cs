namespace Eros.Domain.Aggregates.Buildings;

public interface IBuildingWriteRepository
{
    Task AddAsync(Building building, CancellationToken cancellationToken = default);
    Task UpdateAsync(Building building, CancellationToken cancellationToken = default);
    Task DeleteAsync(Building building, CancellationToken cancellationToken = default);
}