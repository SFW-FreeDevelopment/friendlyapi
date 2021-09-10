using System.ComponentModel.DataAnnotations;

namespace FriendlyApi.Service.Models.Requests
{
    public class UserCreateRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}