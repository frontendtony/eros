using System.ComponentModel.DataAnnotations;

namespace EstateManager.Models
{
    public class AuthenticationRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public AuthenticationRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
