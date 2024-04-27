using Eros.Domain.Aggregates.Estates;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Data.Estates.Repositories;

public class EstateUserReadRepository(
    ErosDbContext dbContext
) : IEstateUserReadRepository
{
    public async Task<EstateUser?> GetByEstateIdAndUserIdAsync(Guid estateId, Guid userId,
        CancellationToken cancellationToken)
    {
        return await dbContext.EstateUser
            .Include(eu => eu.Estate)
            .Include(eu => eu.User)
            .Include(eu => eu.Role)
            .FirstOrDefaultAsync(eu => eu.EstateId == estateId && eu.UserId == userId, cancellationToken);
    }
    
    public async Task<EstateUser[]> GetEstateUsersByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await dbContext.EstateUser
            .Include(eu => eu.Estate)
            .Include(eu => eu.User)
            .Include(eu => eu.Role)
            .ThenInclude(r => r.Permissions)
            .Where(eu => eu.UserId == userId)
            .ToArrayAsync(cancellationToken);
    }
}