using System.ComponentModel.DataAnnotations;

namespace FriendlyApi.Service.Models.Requests
{
    public class UserUpdateRequest
    {
        [MinLength(2)]
        [MaxLength(30)]
        public string Username { get; set; }
        
        [MinLength(6)]
        [MaxLength(20)]
        public string Password { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        
        [Phone]
        public string PhoneNumber { get; set; }
    }
}