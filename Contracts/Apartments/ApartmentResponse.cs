namespace Eros.Contracts.Apartments;

public record ApartmentResponse(
    string Id,
    string Name,
    string Description,
    string ApartmentTypeId,
    string Floor,
    string UnitNumber,
    int NumberOfRooms,
    string BuildingId,
    string CreatedBy,
    string CreatedAt,
    string UpdatedAt
);
