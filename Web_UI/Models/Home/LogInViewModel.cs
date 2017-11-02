using System.ComponentModel.DataAnnotations;

namespace Web_UI.Models.Home
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
