using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FriendlyApi.Service.Models;
using FriendlyApi.Service.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FriendlyApi.Service.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IMongoRepository<User> _repository;
        
        public UsersController(IMongoRepository<User> repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _repository.GetAll();
        }
        
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<User> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }
        
        [HttpPost]
        public async Task<User> Create(UserCreateRequest request)
        {
            User newUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = request.Username,
                Password = request.Password,
                Email = request.Email
            };
            
            return await _repository.Create(newUser);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<User> Update(Guid id, UserUpdateRequest request)
        {
            User user = await _repository.GetById(id);

            if (!string.IsNullOrEmpty(request.Username))
            {
                user.Username = request.Username;
            }

            if (!string.IsNullOrEmpty(request.Password))
            {
                user.Password = request.Password;
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                user.Email = request.Email;
            }

            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                user.PhoneNumber = request.PhoneNumber;
            }
            
            return await _repository.Update(id, user);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task Delete(Guid id)
        {
            await _repository.Delete(id);
        }
    }
}