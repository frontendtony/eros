using Application.Interfaces;
using EstateManager.Entities;
using EstateManager.Models;

namespace EstateManager.Interfaces;

public interface IEstateWriteRepository : IWriteRepository<Estate>
{
    public Task<EstateBuilding> CreateBuildingAsync(EstateBuilding building);
}
