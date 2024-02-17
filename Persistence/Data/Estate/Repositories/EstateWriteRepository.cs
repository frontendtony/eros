using Eros.Domain.Aggregates.Estates;
using Eros.Domain.Aggregates.Estates.Repositories;

namespace Eros.Persistence.Data.Estates.Repositories;

public class EstateWriteRepository : IEstateWriteRepository
{
    private readonly ErosDbContext _context;

    public EstateWriteRepository(ErosDbContext context)
    {
        _context = context;
    }

    public async Task<Estate> AddAsync(Estate estate, CancellationToken cancellationToken)
    {
        await _context.Estates.AddAsync(estate, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return estate;
    }

    public async Task<Estate> UpdateAsync(Estate estate, CancellationToken cancellationToken)
    {
        _context.Estates.Update(estate);
        await _context.SaveChangesAsync(cancellationToken);

        return estate;
    }

    public async Task DeleteAsync(Estate estate, CancellationToken cancellationToken)
    {
        _context.Estates.Remove(estate);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
