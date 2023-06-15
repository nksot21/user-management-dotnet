using System.ComponentModel.DataAnnotations;

namespace user_management_api.Models
{
    public class UpdateUserModel
    {

        [Required(ErrorMessage = "Require Fullname")]
        [StringLength(255, ErrorMessage = "Fullname exceeds 255 characters")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Require Username")]
        [StringLength(255, ErrorMessage = "Username exceeds 255 characters")]
        public string Username { get; set; }
        [StringLength(255, ErrorMessage = "Email exceeds 255 characters")]
        public string Email { get; set; }

    }
}
