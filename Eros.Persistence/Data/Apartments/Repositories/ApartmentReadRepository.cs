using Eros.Domain.Aggregates.Apartments;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Data.Apartments.Repositories;

public class ApartmentReadRepository : IApartmentReadRepository
{
    private readonly ErosDbContext _dbContext;

    public ApartmentReadRepository(ErosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Apartment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Apartments
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Apartment>> GetByBuildingIdAsync(Guid buildingId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Apartments
            .Where(a => a.BuildingId == buildingId)
            .ToListAsync(cancellationToken);
    }
}
