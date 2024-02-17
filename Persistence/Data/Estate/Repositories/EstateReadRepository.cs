using Eros.Domain.Aggregates.Estates;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Data.Estates.Repositories;

public class EstateReadRepository : IEstateReadRepository
{
    private readonly ErosDbContext _dbContext;

    public EstateReadRepository(ErosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Estate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Estates
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<Estate?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Estates
            .FirstOrDefaultAsync(e => e.Name == name, cancellationToken);
    }

    public async Task<IEnumerable<Estate>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Estates
            .AsNoTracking()
            .Include(e => e.Roles)
            .ToListAsync(cancellationToken);
    }
}
