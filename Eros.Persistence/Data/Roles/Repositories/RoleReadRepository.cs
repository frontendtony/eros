using Eros.Domain.Aggregates.Roles;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Data.Roles.Repositories;

public class RoleReadRepository(ErosDbContext dbContext) : IRoleReadRepository
{
    public async Task<Role?> GetByIdAsync(Guid id)
    {
        return await dbContext.Roles
            .Include(r => r.Permissions)
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

    public async Task<bool> HasPermissionsAsync(Guid roleId, IEnumerable<Guid> permissions, CancellationToken cancellationToken)
    {
        var includedPermissions = await dbContext.Roles
            .Where(r => r.Id == roleId)
            .SelectMany(r => r.Permissions)
            .Select(p => p.Id).ToListAsync(cancellationToken);

        var includedPermissionsCount = includedPermissions.Intersect(permissions).Count();

        return includedPermissionsCount == permissions.Count();
    }
}