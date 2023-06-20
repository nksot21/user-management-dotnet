using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using user_management_api.Repositories;

namespace user_management_api.Entities
{
    [Table("user")]
    public class IndividualUser : IEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Require Fullname")]
        [Column("fullname")]
        [StringLength(255, ErrorMessage = "Fullname exceeds 255 characters")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Require Username")]
        [Column("username")]
        [StringLength(255, ErrorMessage = "Username exceeds 255 characters")]
        public string Username { get; set; }
        [StringLength(255, ErrorMessage = "Email exceeds 255 characters")]
        [Column("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Require Password")]
        [StringLength(255, ErrorMessage = "Password exceeds 255 characters")]
        [Column("password")]
        public string Password { get; set; }


        [Required]
        [Column("salt")]
        public int Salt { get; set; }

        [Column("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Column("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [Column("deletedAt")]
        public DateTime? DeletedAt { get; set; }

        public IndividualUser()
        {
        }
        public IndividualUser(string fullname, string username, string email, string password, int salt)
        {
            Fullname = fullname;
            Username = username;
            Email = email;
            Password = password;
            Salt = salt;
        }


    }
}
