namespace Eros.Api.Contracts.Buildings;

public record UpdateBuildingRequest(
    string Name,
    string Description,
    string BuildingTypeId,
    string Address
);
