using System;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace FriendlyApi.Service.Models
{
    public abstract class BaseResource
    {
        [BsonId]
        public string Id { get; set; }
        
        public string OwnerId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; }
        
        public int Version { get; set; }

        [JsonIgnore]
        public bool Deleted { get; set; }
    }
}