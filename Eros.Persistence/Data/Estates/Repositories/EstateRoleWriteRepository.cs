using Eros.Domain.Aggregates.Estates;

namespace Eros.Persistence.Data.Estates.Repositories;

public class EstateRoleWriteRepository(ErosDbContext dbContext) : IEstateRoleWriteRepository
{
    public Task AddAsync(EstateRole estateRole, CancellationToken cancellationToken = default)
    {
        dbContext.EstateRole.Add(estateRole);

        return Task.CompletedTask;
    }
}