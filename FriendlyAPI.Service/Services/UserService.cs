using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
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
            try
            {
                return await _repository.GetAll();
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
                return await _repository.GetById(id.ToString());
            }
            catch (HttpResponseException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public async Task<User> Create(UserCreateRequest request)
        {
            try
            {
                User newUser = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = request.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber
                };
                
                return await _repository.Create(newUser);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public async Task<User> Update(Guid id, UserUpdateRequest request)
        {
            try
            {
                User user = await _repository.GetById(id.ToString());

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
                await _repository.Delete(id.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}