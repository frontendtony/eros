namespace EstateManager.Models;

public class UpdateEstateRequest
{
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string StateProvince { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string? LatLng { get; set; } = string.Empty;
    public string? CreatedBy { get; set; } = string.Empty;
}
