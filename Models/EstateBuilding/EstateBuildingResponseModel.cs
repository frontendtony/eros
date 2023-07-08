namespace EstateManager.Models;
public class EstateBuildingResponseModel
{
    public string Id { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
