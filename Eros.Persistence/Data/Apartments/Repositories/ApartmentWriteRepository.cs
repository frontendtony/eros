using Eros.Domain.Aggregates.Apartments;

namespace Eros.Persistence.Data.Apartments.Repositories;

public class ApartmentWriteRepository(ErosDbContext dbContext) : IApartmentWriteRepository
{
    public async Task AddAsync(Apartment apartment, CancellationToken cancellationToken = default)
    {
        await dbContext.Apartments.AddAsync(apartment, cancellationToken);
    }

    public async Task UpdateAsync(Apartment apartment, CancellationToken cancellationToken = default)
    {
        dbContext.Apartments.Update(apartment);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Apartment apartment, CancellationToken cancellationToken = default)
    {
        dbContext.Apartments.Remove(apartment);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
