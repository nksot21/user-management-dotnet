using System.ComponentModel.DataAnnotations;

namespace user_management_api.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Require Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Require Password")]
        public string Password { get; set; }
    }
}
