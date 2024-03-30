using Eros.Domain.Aggregates.Buildings;

namespace Eros.Application.Features.Estates.Models;

public record EstateBuildingResponseModel(
    Guid Id,
    string Name,
    string Description,
    string Address,
    Guid EstateId,
    string BuildingType,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
