using Eros.Domain.Aggregates.Estates;

namespace Eros.Persistence.Data.Estates.Repositories;

public class EstateUserWriteRepository(ErosDbContext dbContext) : IEstateUserWriteRepository
{

    public async Task AddAsync(EstateUser estateUser, CancellationToken cancellationToken = default)
    {
        await dbContext.EstateUser.AddAsync(estateUser, cancellationToken);
    }
}
