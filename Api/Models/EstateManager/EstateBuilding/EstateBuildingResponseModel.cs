namespace Api.ResponseModels;

public class EstateBuildingResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = String.Empty;
    public Guid EstateId { get; set; }
    public string? EstateName { get; set; } = string.Empty;
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
