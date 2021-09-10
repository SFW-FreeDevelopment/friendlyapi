using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace FriendlyApi.Service.Models
{
    public abstract class BaseResource
    {
        [BsonId]
        public string Id { get; set; }
        
        [BsonElement("deleted")]
        [JsonIgnore]
        public bool Deleted { get; set; }
    }
}