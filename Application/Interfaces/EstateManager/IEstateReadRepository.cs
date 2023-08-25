using EstateManager.Entities;

namespace EstateManager.Interfaces;

public interface IEstateReadRepository
{
    Task<Estate?> GetById(Guid estateId);
    Task<Estate?> GetByName(string name);
    Task<List<Estate>> GetEstates();
    Task<List<EstateBuilding>> GetEstateBuildings(Guid estateId, int pageSize, int pageNumber);
    Task<EstateBuilding?> GetEstateBuildingById(Guid buildingId, Guid estateId);
    Task<EstateBuilding?> GetEstateBuildingByName(string name, Guid estateId);
}
