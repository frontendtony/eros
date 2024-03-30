using Eros.Domain.Aggregates.Apartments;

namespace Eros.Persistence.Data.Apartments.Repositories;

public class ApartmentWriteRepository : IApartmentWriteRepository
{
    private readonly ErosDbContext _dbContext;

    public ApartmentWriteRepository(ErosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Apartment apartment, CancellationToken cancellationToken = default)
    {
        await _dbContext.Apartments.AddAsync(apartment, cancellationToken);
    }

    public async Task UpdateAsync(Apartment apartment, CancellationToken cancellationToken = default)
    {
        _dbContext.Apartments.Update(apartment);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Apartment apartment, CancellationToken cancellationToken = default)
    {
        _dbContext.Apartments.Remove(apartment);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
