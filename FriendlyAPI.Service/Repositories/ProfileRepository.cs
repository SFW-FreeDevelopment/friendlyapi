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
    public class ProfileRepository : IMongoRepository<Profile>
    {
        private readonly IMongoClient _mongoClient;

        public ProfileRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }
        
        public async Task<IEnumerable<Profile>> GetAll()
        {
            List<Profile> profileCollection = await GetCollection().AsQueryable()
                .Where(u => !u.Deleted).ToListAsync();
            return profileCollection;
        }

        public async Task<Profile> GetById(string id)
        {
            var profile = await GetCollection().AsQueryable()
                .FirstOrDefaultAsync(u => u.Id == id && !u.Deleted);
            return profile;
        }

        public async Task<Profile> GetByOwnerId(string id)
        {
            var profile = await GetCollection().AsQueryable()
                .FirstOrDefaultAsync(u => u.OwnerId == id && !u.Deleted);
            return profile;
        }

        public async Task<Profile> Create(Profile data)
        {
            data.CreatedAt = DateTime.UtcNow;
            data.UpdatedAt = DateTime.UtcNow;
            await GetCollection().InsertOneAsync(data);
            var profileList = await GetCollection().AsQueryable().ToListAsync();
            return profileList.FirstOrDefault(x => x.Id == data.Id);
        }

        public async Task<Profile> Update(string id, Profile data)
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
                await GetCollection().UpdateOneAsync<Profile>(x => x.Id == id,
                    Builders<Profile>.Update.Set(x => x.Deleted, true));
            }
        }
        
        private IMongoCollection<Profile> GetCollection()
        {
            IMongoDatabase database = _mongoClient.GetDatabase("friendlyapi");
            IMongoCollection<Profile> collection = database.GetCollection<Profile>("profiles");
            return collection;
        }
    }
}