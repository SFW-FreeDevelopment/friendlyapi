using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FriendlyApi.Service.Models;
using FriendlyApi.Service.Models.Requests;

namespace FriendlyApi.Service
{
    public interface IMongoRepository<T> where T : BaseResource
    {
        public Task<IEnumerable<User>> GetAll();
        public Task<User> GetById(Guid id);
        public Task<User> Create(T data);
        public Task<User> Update(Guid id, T data);
        public Task<User> Delete(Guid id);
    }
}