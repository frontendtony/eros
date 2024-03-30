using Eros.Domain.Aggregates.Roles;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Data.Roles.Repositories;

public class PermissionReadRepository(ErosDbContext dbContext) : IPermissionReadRepository
{
    private readonly ErosDbContext _dbContext = dbContext;

    public async Task<IEnumerable<Permission>> GetAllAsync()
    {
        return await _dbContext.Permissions
            .AsNoTracking()
            .ToListAsync();
    }
}