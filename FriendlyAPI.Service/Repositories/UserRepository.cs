using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendlyApi.Service.Exceptions;
using FriendlyApi.Service.Models;
using FriendlyApi.Service.Repositories.Interfaces;
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
            List<User> userCollection = await GetCollection().AsQueryable()
                .Where(u => !u.Deleted).ToListAsync();
            return userCollection;
        }

        public async Task<User> GetById(string id)
        {
            var user = await GetCollection().AsQueryable()
                .FirstOrDefaultAsync(u => u.Id == id && !u.Deleted);
            return user ?? throw new NotFoundException(id);
        }

        public Task<User> GetByOwnerId(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> Create(User data)
        {
            data.CreatedAt = DateTime.UtcNow;
            data.UpdatedAt = DateTime.UtcNow;
            try
            {
                await GetCollection().InsertOneAsync(data);
            }
            catch (MongoWriteException mwe)
            {
                var writeError = mwe.WriteError;
                
                switch (writeError.Category)
                {
                    case ServerErrorCategory.DuplicateKey when writeError.Message.Contains("usernameIdx"):
                        throw new InvalidRequestException("User with this username already exists!");
                    case ServerErrorCategory.DuplicateKey when writeError.Message.Contains("emailIdx"):
                        throw new InvalidRequestException("This email address is already in use!");
                    case ServerErrorCategory.Uncategorized:
                        break;
                    case ServerErrorCategory.ExecutionTimeout:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var userList = await GetCollection().AsQueryable().ToListAsync();
            return userList.FirstOrDefault(x => x.Id == data.Id);
        }

        public async Task<User> Update(string id, User data)
        {
            data.UpdatedAt = DateTime.UtcNow;
            data.Version++;
            await GetCollection().UpdateOneAsync<User>(x => x.Id == id,
                Builders<User>.Update.Set(x => x.Username, data.Username).Set(x => x.Password, data.Password)
                    .Set(x => x.Email, data.Email).Set(x => x.PhoneNumber, data.PhoneNumber));

            return await GetCollection().AsQueryable().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Delete(string id, bool hardDelete = false)
        {
            if (hardDelete)
            {
                await GetCollection().DeleteOneAsync(x => x.Id == id);
            }
            else
            {
                await GetCollection().UpdateOneAsync<User>(x => x.Id == id,
                    Builders<User>.Update.Set(x => x.Deleted, true));
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