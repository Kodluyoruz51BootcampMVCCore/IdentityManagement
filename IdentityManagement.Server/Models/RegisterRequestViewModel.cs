using System.ComponentModel.DataAnnotations;

namespace IdentityManagement.Server.Models
{
    public class RegisterRequestViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
