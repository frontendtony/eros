namespace Eros.Contracts.Buildings;

public record BuildingResponse(
    string Id,
    string Name,
    string Description,
    string Address,
    string BuildingTypeId,
    string EstateId,
    string CreatedBy,
    string CreatedAt,
    string UpdatedAt
);