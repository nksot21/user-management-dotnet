using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace user_management_api.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}

        public DateTime?  DeletedAt { get; set; } = DateTime.Now;

        public User()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            DeletedAt = null;
        }
        public User(int id, string fullname, string username, string email, string password)
        {
            Id = id;
            Fullname = fullname;
            Username = username;
            Email = email;
            Password = password;
        }


    }
}
