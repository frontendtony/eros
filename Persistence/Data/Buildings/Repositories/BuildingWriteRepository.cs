using Eros.Domain.Aggregates.Buildings;

namespace Eros.Persistence.Data.Buildings.Repositories;

public class BuildingWriteRepository : IBuildingWriteRepository
{
    private readonly ErosDbContext _context;

    public BuildingWriteRepository(ErosDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Building building, CancellationToken cancellationToken)
    {
        await _context.Buildings.AddAsync(building, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Building building, CancellationToken cancellationToken)
    {
        _context.Buildings.Remove(building);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
