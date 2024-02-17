using Eros.Domain.Aggregates.Estates;
using Eros.Domain.Aggregates.Roles;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Data.Estates.Repositories;

public class EstateReadRepository : IEstateReadRepository
{
    private readonly ErosDbContext _dbContext;

    public EstateReadRepository(ErosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Estate?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Estates
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Estate>> GetAllAsync()
    {
        return await _dbContext.Estates
            .AsNoTracking()
            .Include(e => e.Roles)
            .ToListAsync();
    }
}
