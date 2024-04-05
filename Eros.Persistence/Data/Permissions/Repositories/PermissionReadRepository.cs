using Eros.Domain.Aggregates.Roles;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Data.Permissions.Repositories;

public class PermissionReadRepository(ErosDbContext dbContext) : IPermissionReadRepository
{
    private readonly ErosDbContext _dbContext = dbContext;

    public async Task<IEnumerable<Permission>> GetAllAsync()
    {
        return await _dbContext.Permissions
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<Permission?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Permissions
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}