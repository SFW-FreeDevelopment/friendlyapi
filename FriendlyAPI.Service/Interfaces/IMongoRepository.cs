using System.Collections.Generic;
using System.Threading.Tasks;
using FriendlyApi.Service.Models;

namespace FriendlyApi.Service.Interfaces
{
    public interface IMongoRepository<T> where T : BaseResource
    {
        public Task<IEnumerable<User>> GetAll();
        public Task<User> GetById(string id);
        public Task<User> Create(T data);
        public Task<User> Update(string id, T data);
        public Task Delete(string id);
    }
}