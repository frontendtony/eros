using Eros.Domain.Aggregates.Buildings;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Data.Buildings.Repositories;

public class BuildingTypeWriteRepository(ErosDbContext dbContext) : IBuildingTypeWriteRepository
{
    public async Task AddAsync(BuildingType buildingType, CancellationToken cancellationToken = default)
    {
        await dbContext.BuildingTypes.AddAsync(buildingType, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(BuildingType buildingType, CancellationToken cancellationToken = default)
    {
        dbContext.BuildingTypes.Update(buildingType);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var buildingType = await dbContext.BuildingTypes
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (buildingType is null) return;

        // only delete if there are no buildings with this type
        var buildings = await dbContext.Buildings
            .FirstOrDefaultAsync(b => b.BuildingTypeId == id, cancellationToken);

        if (buildings is not null) return;

        dbContext.BuildingTypes.Remove(buildingType);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
