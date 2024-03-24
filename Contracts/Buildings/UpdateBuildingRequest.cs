namespace Eros.Contracts.Buildings;

public record UpdateBuildingRequest(
    string Name,
    string Description,
    string BuildingTypeId,
    string Address
);
