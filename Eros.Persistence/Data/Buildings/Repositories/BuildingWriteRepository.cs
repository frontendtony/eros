using Eros.Domain.Aggregates.Buildings;

namespace Eros.Persistence.Data.Buildings.Repositories;

public class BuildingWriteRepository(ErosDbContext context) : IBuildingWriteRepository
{
    public async Task AddAsync(Building building, CancellationToken cancellationToken)
    {
        await context.Buildings.AddAsync(building, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Building building, CancellationToken cancellationToken)
    {
        context.Buildings.Update(building);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Building building, CancellationToken cancellationToken)
    {
        context.Buildings.Remove(building);
        await context.SaveChangesAsync(cancellationToken);
    }
}
