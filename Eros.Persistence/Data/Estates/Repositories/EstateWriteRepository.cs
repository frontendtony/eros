using Eros.Domain.Aggregates.Estates.Repositories;

namespace Eros.Persistence.Data.Estates.Repositories;

public class EstateWriteRepository(ErosDbContext context) : IEstateWriteRepository
{
    public async Task<Domain.Aggregates.Estates.Estate> AddAsync(Domain.Aggregates.Estates.Estate estate, CancellationToken cancellationToken)
    {
        await context.Estates.AddAsync(estate, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return estate;
    }

    public async Task<Domain.Aggregates.Estates.Estate> UpdateAsync(Domain.Aggregates.Estates.Estate estate, CancellationToken cancellationToken)
    {
        context.Estates.Update(estate);
        await context.SaveChangesAsync(cancellationToken);

        return estate;
    }

    public async Task DeleteAsync(Domain.Aggregates.Estates.Estate estate, CancellationToken cancellationToken)
    {
        context.Estates.Remove(estate);
        await context.SaveChangesAsync(cancellationToken);
    }
}
