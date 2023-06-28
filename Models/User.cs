using System.ComponentModel.DataAnnotations;

namespace EstateManager.Models
{
    public class UserModel
    {
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Email { get; set; }
    }
}
