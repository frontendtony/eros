using Eros.Domain.Aggregates.Roles;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Data.Roles.Repositories;

public class RoleReadRepository : IRoleReadRepository
{
    private readonly ErosDbContext _dbContext;

    public RoleReadRepository(ErosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Role?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Roles
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await _dbContext.Roles
            .AsNoTracking()
            .ToListAsync();
    }
}