using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendlyApi.Service.Interfaces;
using FriendlyApi.Service.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace FriendlyApi.Service.Repositories
{
    public class UserRepository : IMongoRepository<User>
    {
        private readonly IMongoClient _mongoClient;

        public UserRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                List<User> userCollection = await GetCollection().AsQueryable().ToListAsync();
                return userCollection.Where(x => x.Deleted == false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<User> GetById(string id)
        {
            try
            {
                var userList = await GetCollection().AsQueryable().ToListAsync();
                return userList.FirstOrDefault(x => x.Id == id.ToString() && x.Deleted == false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<User> Create(User data)
        {
            try
            {
                await GetCollection().InsertOneAsync(data);
                var userList = await GetCollection().AsQueryable().ToListAsync();
                return userList.FirstOrDefault(x => x.Id == data.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<User> Update(string id, User data)
        {
            try
            {
                await GetCollection().UpdateOneAsync<User>(x => x.Id == id.ToString(),
                    Builders<User>.Update.Set(x => x.Username, data.Username).Set(x => x.Password, data.Password)
                        .Set(x => x.Email, data.Email).Set(x => x.PhoneNumber, data.PhoneNumber));
                var userList = await GetCollection().AsQueryable().ToListAsync();
                return userList.FirstOrDefault(x => x.Id == data.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Delete(string id)
        {
            try
            {
                await GetCollection().UpdateOneAsync<User>(x => x.Id == id.ToString(),
                    Builders<User>.Update.Set(x => x.Deleted, true));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private IMongoCollection<User> GetCollection()
        {
            IMongoDatabase database = _mongoClient.GetDatabase("friendlyapi");
            IMongoCollection<User> collection = database.GetCollection<User>("users");
            return collection;
        }
    }
}