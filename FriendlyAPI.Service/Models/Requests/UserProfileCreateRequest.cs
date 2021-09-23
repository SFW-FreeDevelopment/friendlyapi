using System;
using System.ComponentModel.DataAnnotations;

namespace FriendlyApi.Service.Models.Requests
{
    public class UserProfileCreateRequest
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        public string Biography { get; set; }
        
        public string AvatarUrl { get; set; }
    }
}