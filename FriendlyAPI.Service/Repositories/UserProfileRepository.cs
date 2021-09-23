using System.Collections.Generic;
using System.Threading.Tasks;
using FriendlyApi.Service.Models;
using FriendlyApi.Service.Repositories.Interfaces;

namespace FriendlyApi.Service.Repositories
{
    public class UserProfileRepository : IMongoRepository<UserProfile>
    {
        public Task<IEnumerable<UserProfile>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<UserProfile> GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserProfile> Create(UserProfile data)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserProfile> Update(string id, UserProfile data)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}