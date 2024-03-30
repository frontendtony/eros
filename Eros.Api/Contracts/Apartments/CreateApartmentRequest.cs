namespace Eros.Api.Contracts.Apartments;

public record CreateApartmentRequest(
    string Name,
    string Description,
    string ApartmentTypeId,
    string Floor,
    string UnitNumber,
    int NumberOfRooms,
    string BuildingId
);
