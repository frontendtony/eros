namespace EstateManager.Commands;

public class UpdateEstateCommand : EstateBaseCommand
{
    public new string? Name { get; set; }
    public new string? Country { get; set; }
    public new string? State { get; set; }
    public new string? City { get; set; }
    public new string? Address { get; set; }
    public new string? ZipCode { get; set; }
    public new string? LatLng { get; set; }
}
