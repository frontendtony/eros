using Eros.Domain.Aggregates.Buildings;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Data.Buildings.Repositories;

public class BuildingTypeWriteRepository : IBuildingTypeWriteRepository
{
    private readonly ErosDbContext _dbContext;

    public BuildingTypeWriteRepository(ErosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(BuildingType buildingType, CancellationToken cancellationToken = default)
    {
        await _dbContext.BuildingTypes.AddAsync(buildingType, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(BuildingType buildingType, CancellationToken cancellationToken = default)
    {
        _dbContext.BuildingTypes.Update(buildingType);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var buildingType = await _dbContext.BuildingTypes
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (buildingType is null) return;

        // only delete if there are no buildings with this type
        var buildings = await _dbContext.Buildings
            .FirstOrDefaultAsync(b => b.BuildingTypeId == id, cancellationToken);

        if (buildings is not null) return;

        _dbContext.BuildingTypes.Remove(buildingType);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
