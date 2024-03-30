namespace Eros.Domain.Aggregates.Buildings;

public interface IBuildingTypeWriteRepository
{
    Task AddAsync(BuildingType buildingType, CancellationToken cancellationToken = default);
    Task UpdateAsync(BuildingType buildingType, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
