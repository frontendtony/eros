namespace Eros.Api.Contracts.Buildings;

public record BuildingResponse(
    Guid Id,
    string Name,
    string Description,
    string Address,
    string BuildingType,
    Guid EstateId,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);