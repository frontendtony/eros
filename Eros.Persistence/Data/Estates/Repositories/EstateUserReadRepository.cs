using Eros.Domain.Aggregates.Estates;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Data.Estates.Repositories;

public class EstateUserReadRepository(
    ErosDbContext _dbContext
) : IEstateUserReadRepository
{
    public async Task<EstateUser?> GetByEstateIdAndUserIdAsync(Guid estateId, Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.EstateUser
            .FirstOrDefaultAsync(eu => eu.EstateId == estateId && eu.UserId == userId, cancellationToken);
    }
}
