using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace FriendlyApi.Service.Models
{
    public class User : BaseResource
    {
        [BsonElement("username")]
        public string Username { get; set; }
        
        [BsonElement("password")]
        public string Password { get; set; }
        
        [BsonElement("email")]
        public string Email { get; set; }
        
        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; }
    }
}