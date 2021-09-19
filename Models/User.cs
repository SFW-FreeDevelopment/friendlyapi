namespace FriendlyApi.Service.Models
{
    public class User : BaseResource
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}