namespace user_management_api.Models
{
    public class UserResponseModel
    {

        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }

        public UserResponseModel() { }
        public UserResponseModel(string fullname, string username, string email, DateTime createdAt, DateTime lastModifiedAt)
        {
            Fullname = fullname;
            Username = username;
            Email = email;
            CreatedAt = createdAt;
            LastModifiedAt = lastModifiedAt;
        }
    }
}
