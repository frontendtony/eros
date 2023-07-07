using System.ComponentModel.DataAnnotations;

namespace EstateManager.Models;
public class CreateEstateBuildingModel
{

    [Required]
    public string Name { get; set; } = String.Empty;

    public string? Description { get; set; } = String.Empty;
}