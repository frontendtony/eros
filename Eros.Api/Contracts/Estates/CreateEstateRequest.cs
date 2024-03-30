namespace Eros.Api.Contracts.Estates;

public record CreateEstateRequest(
    string Name,
    string Address,
    string CountryId,
    string StateId,
    string CityId,
    string ZipCode,
    string LatLng
);
