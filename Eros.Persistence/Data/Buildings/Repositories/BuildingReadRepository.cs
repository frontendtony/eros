using Eros.Domain.Aggregates.Apartments;
using Eros.Domain.Aggregates.Buildings;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Data.Buildings.Repositories;

public class BuildingReadRepository(ErosDbContext dbContext) : IBuildingReadRepository
{
    public async Task<Building?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Buildings
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<Building?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await dbContext.Buildings
            .FirstOrDefaultAsync(e => e.Name.ToLower() == name.ToLower(), cancellationToken);
    }

    public async Task<IEnumerable<Building>> GetByEstateIdAsync(Guid estateId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Buildings
            .Where(b => b.EstateId == estateId)
            .Include(b => b.BuildingType)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Apartment>> GetApartmentsAsync(Guid buildingId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Apartments
            .Where(a => a.BuildingId == buildingId)
            .ToListAsync(cancellationToken);
    }
}
