using EstateManager.Constants;

namespace EstateManager.Commands;

public abstract class EstateBaseCommand
{
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = GlobalConstants.DefaultCountry;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string? LatLng { get; set; } = string.Empty;
}
