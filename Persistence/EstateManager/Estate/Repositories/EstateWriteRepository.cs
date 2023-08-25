using EstateManager.Entities;
using EstateManager.Interfaces;
using EstateManager.Repositories;

public class EstateWriteRepository : IEstateWriteRepository
{
    private EstateManagerDbContext _dbContext;

    public EstateWriteRepository(EstateManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Estate Create(Estate estate)
    {
        return _dbContext.Estates.Add(estate).Entity;
    }

    public Estate Update(Estate estate)
    {
        return _dbContext.Estates.Update(estate).Entity;
    }

    public void Delete(Estate estate)
    {
        _dbContext.Estates.Remove(estate);
    }

    public async Task<EstateBuilding> CreateBuildingAsync(EstateBuilding building)
    {
        var newBuilding = await _dbContext.EstateBuildings.AddAsync(building);
        return newBuilding.Entity;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
