namespace Eros.Contracts.Estates;

public record EstateResponse(
    Guid Id,
    string Name,
    string Address,
    string? LatLng,
    Guid CreatedBy,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
