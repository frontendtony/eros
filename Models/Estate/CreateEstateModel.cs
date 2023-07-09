using System.ComponentModel.DataAnnotations;
using EstateManager.Constants;

namespace EstateManager.Models
{
    public class CreateEstateModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Country { get; set; } = GlobalConstants.DefaultCountry;
        [Required]
        public string State { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string? LatLng { get; set; } = string.Empty;
    }
}
