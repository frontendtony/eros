namespace Eros.Api.Contracts.Buildings;

public record CreateBuildingRequest(
    string Name,
    string Description,
    string EstateId,
    string BuildingTypeId,
    string Address
);
