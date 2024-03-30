using Eros.Domain.Aggregates.Apartments;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Data.Apartments.Repositories;

public class ApartmentReadRepository(ErosDbContext dbContext) : IApartmentReadRepository
{
    public async Task<Apartment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Apartments
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Apartment>> GetByBuildingIdAsync(Guid buildingId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Apartments
            .Where(a => a.BuildingId == buildingId)
            .ToListAsync(cancellationToken);
    }
}
