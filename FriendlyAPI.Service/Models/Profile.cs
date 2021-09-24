using System;
using FriendlyApi.Service.Models.Requests;

namespace FriendlyApi.Service.Models
{
    public class Profile : BaseResource
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        public string Biography { get; set; }
        
        public string AvatarUrl { get; set; }
        
        public Profile() { }
        public Profile(string ownerId, ProfileCreateRequest request)
        {
            Id = ownerId;
            OwnerId = ownerId;
            FirstName = request.FirstName;
            LastName = request.LastName;
            DateOfBirth = request.DateOfBirth;
            Biography = request.Biography;
            AvatarUrl = request.AvatarUrl;
        }
    }
}