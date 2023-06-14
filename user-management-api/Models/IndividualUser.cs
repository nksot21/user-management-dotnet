﻿using System.Text.Json.Serialization;

namespace user_management_api.Models
{
    public class IndividualUser
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Nullable<DateTime> DeletedAt { get; set; }

        [JsonConstructor]
        public IndividualUser()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            DeletedAt = null;
        }
        public IndividualUser(int id, string fullname, string username, string email, string password)
        {
            Id = id;
            Fullname = fullname;
            Username = username;
            Email = email;
            Password = password;
        }


    }
}