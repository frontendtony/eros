using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EstateManager.Entities;

[Index(nameof(Name), IsUnique = true)]
public class EstateBuilding
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public Guid EstateId { get; set; }

    [Required]
    public Guid CreatedBy { get; set; }

    [Required]
    public string Name { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;
}
