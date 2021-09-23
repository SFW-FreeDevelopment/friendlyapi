using System;
using FriendlyApi.Service.Models.Requests;

namespace FriendlyApi.Service.Models
{
    public class User : BaseResource
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserProfile Profile { get; set; }
        
        public User() { }
        public User(UserCreateRequest request)
        {
            Id = Guid.NewGuid().ToString();
            OwnerId = Id;
            Username = request.Username;
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            Email = request.Email;
            PhoneNumber = request.PhoneNumber;
        }
    }
}