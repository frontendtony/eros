using Eros.Domain.Aggregates.Buildings;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Data.Buildings.Repositories;

public class BuildingTypeReadRepository : IBuildingTypeReadRepository
{
    private readonly ErosDbContext _dbContext;

    public BuildingTypeReadRepository(ErosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BuildingType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.BuildingTypes
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<BuildingType?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.BuildingTypes
            .FirstOrDefaultAsync(e => e.Name == name, cancellationToken);
    }

    public async Task<IEnumerable<BuildingType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.BuildingTypes
            .ToListAsync(cancellationToken);
    }
}