using Eros.Domain.Aggregates.Buildings;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Data.Buildings.Repositories;

public class BuildingTypeReadRepository(ErosDbContext dbContext) : IBuildingTypeReadRepository
{
    public async Task<BuildingType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.BuildingTypes
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<BuildingType?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await dbContext.BuildingTypes
            .FirstOrDefaultAsync(e => e.Name == name, cancellationToken);
    }

    public async Task<IEnumerable<BuildingType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.BuildingTypes
            .ToListAsync(cancellationToken);
    }
}