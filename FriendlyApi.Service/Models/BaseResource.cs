using MongoDB.Bson.Serialization.Attributes;

namespace FriendlyApi.Service.Models
{
    public abstract class BaseResource
    {
        [BsonId]
        public string Id { get; set; }
    }
}