using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using FriendlyApi.Service.Exceptions;
using FriendlyApi.Service.Interfaces;
using FriendlyApi.Service.Models;
using FriendlyApi.Service.Models.Requests;

namespace FriendlyApi.Service.Services
{
    public class UserService
    {
        private readonly IMongoRepository<User> _repository;

        public UserService(IMongoRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _repository.GetAll();
        }
        
        public async Task<User> GetById(Guid id)
        {
            var user = await _repository.GetById(id.ToString());
            
            if (user == null)
                throw new NotFoundException(id);
            
            return user;
        }
        
        public async Task<User> Create(UserCreateRequest request)
        {
            var user = new User(request);

            return await _repository.Create(user);
        }
        
        public async Task<User> Update(Guid id, UserUpdateRequest request)
        {
            var user = await _repository.GetById(id.ToString());
            
            if (user == null)
                throw new NotFoundException(id);

            if (!string.IsNullOrEmpty(request.Username))
            {
                user.Username = request.Username;
            }

            if (!string.IsNullOrEmpty(request.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                user.Email = request.Email;
            }

            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                user.PhoneNumber = request.PhoneNumber;
            }
                
            return await _repository.Update(id.ToString(), user);
        }
        
        public async Task Delete(Guid id)
        {
            await _repository.Delete(id.ToString());
        }
    }
}