namespace Eros.Contracts.Apartments;

public record UpdateApartmentRequest(
    string Name,
    string Description,
    string ApartmentTypeId,
    string UnitNumber,
    int NumberOfRooms
);
