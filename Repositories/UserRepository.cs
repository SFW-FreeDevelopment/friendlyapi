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
            _settings = MongoClientSettings.FromConnectionString(
                "mongodb+srv://admin:f6U5vOzQN29Xpvpv@cluster0.xqhm9.mongodb.net/friendlyapi?connect=replicaSet&retryWrites=true&w=majority");
            _client = new MongoClient(_settings);
            _database = _client.GetDatabase("friendlyapi");
            _userCollection = _database.GetCollection<User>("users");
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                List<User> userCollection = await _userCollection.AsQueryable().ToListAsync();
                return userCollection.Where(x => x.Deleted == false);
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

        public async Task<User> Update(Guid id, User data)
        {
            try
            {
                await _userCollection.UpdateOneAsync<User>(x => x.Id == id.ToString(),
                    Builders<User>.Update.Set(x => x.Username, data.Username).Set(x => x.Password, data.Password)
                        .Set(x => x.Email, data.Email).Set(x => x.PhoneNumber, data.PhoneNumber));
                var userList = await _userCollection.AsQueryable().ToListAsync();
                return userList.FirstOrDefault(x => x.Id == data.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                await _userCollection.UpdateOneAsync<User>(x => x.Id == id.ToString(),
                    Builders<User>.Update.Set(x => x.Deleted, true));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}