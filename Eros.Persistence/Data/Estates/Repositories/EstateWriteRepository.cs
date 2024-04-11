using Eros.Domain.Aggregates.Estates;

namespace Eros.Persistence.Data.Estates.Repositories;

public class EstateWriteRepository(ErosDbContext context) : IEstateWriteRepository
{
    public async Task<Estate> AddAsync(Estate estate, CancellationToken cancellationToken)
    {
        await context.Estates.AddAsync(estate, cancellationToken);

        return estate;
    }

    public Estate UpdateAsync(Estate estate, CancellationToken cancellationToken)
    {
        context.Estates.Update(estate);

        return estate;
    }

    public Task DeleteAsync(Estate estate, CancellationToken cancellationToken)
    {
        context.Estates.Remove(estate);
        return Task.CompletedTask;
    }
}
