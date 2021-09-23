using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FriendlyApi.Service.Exceptions;
using FriendlyApi.Service.Models;
using FriendlyApi.Service.Models.Requests;
using FriendlyApi.Service.Repositories.Interfaces;

namespace FriendlyApi.Service.Services
{
    public class UserService
    {
        private readonly IMongoRepository<User> _repository;
        private readonly IMongoRepository<UserProfile> _profileRepository;

        public UserService(IMongoRepository<User> repository, IMongoRepository<UserProfile> profileRepository)
        {
            _repository = repository;
            _profileRepository = profileRepository;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _repository.GetAll();
        }
        
        public async Task<User> GetById(Guid id)
        {
            var user = await _repository.GetById(id.ToString());

            try
            {
                user.Profile = await _profileRepository.GetById(id.ToString());
            }
            catch (Exception)
            {
                user.Profile = null;
            }

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
        
        public async Task Delete(Guid id, bool hardDelete = false)
        {
            await _repository.Delete(id.ToString(), hardDelete);
        }
    }
}