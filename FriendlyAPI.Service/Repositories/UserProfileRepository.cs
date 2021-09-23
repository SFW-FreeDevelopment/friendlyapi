using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendlyApi.Service.Models;
using FriendlyApi.Service.Repositories.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace FriendlyApi.Service.Repositories
{
    public class UserProfileRepository : IMongoRepository<UserProfile>
    {
        private readonly IMongoClient _mongoClient;

        public UserProfileRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }
        
        public async Task<IEnumerable<UserProfile>> GetAll()
        {
            List<UserProfile> userProfileCollection = await GetCollection().AsQueryable()
                .Where(u => !u.Deleted).ToListAsync();
            return userProfileCollection;
        }

        public async Task<UserProfile> GetById(string id)
        {
            var profile = await GetCollection().AsQueryable()
                .FirstOrDefaultAsync(u => u.Id == id && !u.Deleted);
            return profile;
        }

        public async Task<UserProfile> GetByOwnerId(string id)
        {
            var profile = await GetCollection().AsQueryable()
                .FirstOrDefaultAsync(u => u.OwnerId == id && !u.Deleted);
            return profile;
        }

        public async Task<UserProfile> Create(UserProfile data)
        {
            data.CreatedAt = DateTime.UtcNow;
            data.UpdatedAt = DateTime.UtcNow;
            await GetCollection().InsertOneAsync(data);
            var userProfileList = await GetCollection().AsQueryable().ToListAsync();
            return userProfileList.FirstOrDefault(x => x.Id == data.Id);
        }

        public async Task<UserProfile> Update(string id, UserProfile data)
        {
            data.UpdatedAt = DateTime.UtcNow;
            data.Version++;
            await GetCollection().ReplaceOneAsync(x => x.Id == id, data);
            return data;
        }

        public async Task Delete(string id, bool hardDelete = false)
        {
            if (hardDelete)
            {
                await GetCollection().DeleteOneAsync(x => x.Id == id);
            }
            else
            {
                await GetCollection().UpdateOneAsync<UserProfile>(x => x.Id == id,
                    Builders<UserProfile>.Update.Set(x => x.Deleted, true));
            }
        }
        
        private IMongoCollection<UserProfile> GetCollection()
        {
            IMongoDatabase database = _mongoClient.GetDatabase("friendlyapi");
            IMongoCollection<UserProfile> collection = database.GetCollection<UserProfile>("userProfiles");
            return collection;
        }
    }
}