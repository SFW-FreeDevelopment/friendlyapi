using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendlyApi.Service.Models;
using MongoDB.Driver;

namespace FriendlyApi.Service.Repositories
{
    public class UserRepository : IMongoRepository<User>
    {
        private static MongoClientSettings _settings;
        private static MongoClient _client;
        private static IMongoDatabase _database;
        private static IMongoCollection<User> _userCollection;

        public UserRepository()
        {
            _settings = MongoClientSettings.FromConnectionString("mongodb+srv://admin:f6U5vOzQN29Xpvpv@cluster0.xqhm9.mongodb.net/friendlyapi?connect=replicaSet&retryWrites=true&w=majority");
            _client = new MongoClient(_settings);
            _database = _client.GetDatabase("friendlyapi");
            _userCollection = _database.GetCollection<User>("users");
        }
        
        public async Task<IEnumerable<User>> GetAll()
        {
            try
            { 
                return await _userCollection.AsQueryable().ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<User> GetById(Guid id)
        {
            try
            {
                var userList = await _userCollection.AsQueryable().ToListAsync();
                return userList.FirstOrDefault(x => x.Id == id.ToString());
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
                await _userCollection.InsertOneAsync(data);
                var userList = await _userCollection.AsQueryable().ToListAsync();
                return userList.FirstOrDefault(x => x.Id == data.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Task<User> Update(Guid id, User data)
        {
            throw new NotImplementedException();
        }

        public Task<User> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}