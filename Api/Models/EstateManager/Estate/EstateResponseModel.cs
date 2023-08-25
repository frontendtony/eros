using EstateManager.Entities;

namespace Api.ResponseModels;

public class EstateResponseModel
{
    public EstateResponseModel(Estate estate)
    {
        Id = estate.Id;
        Name = estate.Name;
        Country = estate.Country;
        State = estate.State;
        City = estate.City;
        Address = estate.Address;
        ZipCode = estate.ZipCode;
        LatLng = estate.LatLng;
        CreatedBy = estate.CreatedBy;
        CreatedAt = estate.CreatedAt;
        UpdatedAt = estate.UpdatedAt;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string? LatLng { get; set; } = string.Empty;
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
