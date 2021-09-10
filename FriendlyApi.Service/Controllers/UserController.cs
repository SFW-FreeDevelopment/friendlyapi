using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FriendlyApi.Service.Models;
using FriendlyApi.Service.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FriendlyApi.Service.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IMongoRepository<User> _repository;
        
        public UserController(IMongoRepository<User> repository)
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
        public async Task<User> Update(Guid id, User request)
        {
            return await _repository.Update(id, request);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<User> Delete(Guid id)
        {
            return await _repository.Delete(id);
        }
    }
}