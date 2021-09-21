using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FriendlyApi.Service.Models;
using FriendlyApi.Service.Models.Requests;

namespace FriendlyApi.Service.Services.Interfaces
{
    public interface IResourceService<T> where T : BaseResource
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(Guid id);
        Task<User> Create(UserCreateRequest request);
        Task<User> Update(Guid id, UserUpdateRequest request);
        Task Delete(Guid id);
    }
}