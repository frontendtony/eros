using EstateManager.Constants;
using EstateManager.Entities;
using EstateManager.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EstateManager.Repositories;

public class EstateReadRepository : IEstateReadRepository
{
    private readonly EstateManagerDbContext _dbContext;

    public EstateReadRepository(EstateManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Estate?> GetById(Guid estateId)
    {
        return await _dbContext.Estates.FindAsync(estateId);
    }

    public async Task<Estate?> GetByName(string name)
    {
        return await _dbContext.Estates.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<List<Estate>> GetEstates()
    {
        return await _dbContext.Estates.ToListAsync();
    }

    public async Task<List<EstateBuilding>> GetEstateBuildings(Guid estateId, int pageSize, int pageNumber)
    {
        pageNumber = Math.Max(1, pageNumber);
        pageSize = Math.Min(pageSize, GlobalConstants.MaxPageSize);

        var estateBuildings = await _dbContext.EstateBuildings
            .Where(x => x.EstateId == estateId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return estateBuildings;
    }

    public async Task<EstateBuilding?> GetEstateBuildingById(Guid buildingId, Guid estateId)
    {
        return await _dbContext.EstateBuildings.FirstOrDefaultAsync(x => x.Id == buildingId && x.EstateId == estateId);
    }

    public async Task<EstateBuilding?> GetEstateBuildingByName(string name, Guid estateId)
    {
        return await _dbContext.EstateBuildings.FirstOrDefaultAsync(x => x.Name == name && x.EstateId == estateId);
    }
}
