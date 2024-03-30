using Eros.Domain.Aggregates.Apartments;
using Eros.Domain.Aggregates.Buildings;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Data.Buildings.Repositories;

public class BuildingReadRepository : IBuildingReadRepository
{
    private readonly ErosDbContext _dbContext;

    public BuildingReadRepository(ErosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Building?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Buildings
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<Building?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Buildings
            .FirstOrDefaultAsync(e => e.Name.ToLower() == name.ToLower(), cancellationToken);
    }

    public async Task<IEnumerable<Building>> GetByEstateIdAsync(Guid estateId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Buildings
            .Where(b => b.EstateId == estateId)
            .Include(b => b.BuildingType)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Apartment>> GetApartmentsAsync(Guid buildingId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Apartments
            .Where(a => a.BuildingId == buildingId)
            .ToListAsync(cancellationToken);
    }
}
