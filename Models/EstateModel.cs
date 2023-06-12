namespace EstateManager.Models
{
    public record EstateModel
    (
        Guid Id,
        string Name,
        string Country,
        string StateProvince,
        string City,
        string Address,
        string ZipCode,
        string? LatLng
    );
}
