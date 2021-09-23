using System;

namespace FriendlyApi.Service.Models
{
    public class UserProfile : BaseResource
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Biography { get; set; }
        public string AvatarUrl { get; set; }
    }
}