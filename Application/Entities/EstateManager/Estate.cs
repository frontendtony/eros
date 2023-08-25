using EstateManager.Commands;

namespace EstateManager.Entities;
public class Estate
{
    public Estate()
    {
    }

    public Estate(CreateEstateCommand command, Guid createdBy)
    {
        Id = Guid.NewGuid();
        CreatedBy = createdBy;
        Name = command.Name;
        Country = command.Country;
        State = command.State;
        City = command.City;
        Address = command.Address;
        ZipCode = command.ZipCode;
        LatLng = command.LatLng;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; set; }
    public Guid CreatedBy { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string? LatLng { get; set; } = string.Empty;
    public IEnumerable<EstateBuilding> Buildings { get; } = new List<EstateBuilding>();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public void Update(UpdateEstateCommand command)
    {
        Name = command.Name ?? Name;
        Country = command.Country ?? Country;
        State = command.State ?? State;
        City = command.City ?? City;
        Address = command.Address ?? Address;
        ZipCode = command.ZipCode ?? ZipCode;
        LatLng = command.LatLng ?? LatLng;
        UpdatedAt = DateTime.UtcNow;
    }
}
