namespace EstateManager.Commands;

public abstract class EstateBuildingBaseCommand
{
    public required string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
