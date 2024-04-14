using Eros.Domain.Aggregates.Roles;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Data.Roles.Repositories;

public class RoleReadRepository(ErosDbContext dbContext) : IRoleReadRepository
{
    public async Task<Role?> GetByIdAsync(Guid id)
    {
        return await dbContext.Roles
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await dbContext.Roles
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Role>> GetSharedRolesAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Roles
            .AsNoTracking()
            .Where(r => r.IsShared).ToListAsync(cancellationToken);
    }

    public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await dbContext.Roles
            .FirstOrDefaultAsync(r => r.Name == name, cancellationToken);
    }
}