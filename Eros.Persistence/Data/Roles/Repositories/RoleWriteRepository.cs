using Eros.Domain.Aggregates.Roles;

namespace Eros.Persistence.Data.Roles.Repositories;

public class RoleWriteRepository(ErosDbContext dbContext) : IRoleWriteRepository
{
    public async Task<Role?> AddAsync(Role role, CancellationToken cancellationToken = default)
    {
        await dbContext.Roles.AddAsync(role, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return role;
    }

    public async Task<Role?> UpdateAsync(Role role, CancellationToken cancellationToken = default)
    {
        dbContext.Roles.Update(role);
        await dbContext.SaveChangesAsync(cancellationToken);

        return role;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var role = await dbContext.Roles.FindAsync(new object[] { id }, cancellationToken);

        if (role is null)
        {
            return false;
        }

        dbContext.Roles.Remove(role);
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}