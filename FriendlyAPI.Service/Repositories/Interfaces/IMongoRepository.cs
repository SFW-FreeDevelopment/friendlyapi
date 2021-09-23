using System.Collections.Generic;
using System.Threading.Tasks;
using FriendlyApi.Service.Models;

namespace FriendlyApi.Service.Repositories.Interfaces
{
    public interface IMongoRepository<T> where T : BaseResource
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(string id);
        public Task<T> GetByOwnerId(string id);
        public Task<T> Create(T data);
        public Task<T> Update(string id, T data);
        public Task Delete(string id, bool hardDelete = false);
    }
}