namespace FriendlyApi.Service.Models.Requests
{
    public class UserUpdateRequest
    {
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
    }
}