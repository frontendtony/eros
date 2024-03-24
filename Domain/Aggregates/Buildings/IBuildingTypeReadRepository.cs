namespace Eros.Domain.Aggregates.Buildings;

public interface IBuildingTypeReadRepository
{
    Task<BuildingType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<BuildingType?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<BuildingType>> GetAllAsync(CancellationToken cancellationToken = default);
}
