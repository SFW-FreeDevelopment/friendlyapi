using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FriendlyApi.Service.Exceptions;
using FriendlyApi.Service.Models;
using FriendlyApi.Service.Models.Requests;
using FriendlyApi.Service.Repositories.Interfaces;

namespace FriendlyApi.Service.Services
{
    public class UserProfileService
    {
        private readonly IMongoRepository<UserProfile> _repository;

        public UserProfileService(IMongoRepository<UserProfile> repository)
        {
            _repository = repository;
        }
        
        public async Task<IEnumerable<UserProfile>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<UserProfile> GetById(Guid id)
        {
            var userProfile = await _repository.GetById(id.ToString());
            
            if (userProfile == null)
                throw new NotFoundException(id);
            
            return userProfile;
        }

        public async Task<UserProfile> Create(Guid ownerId, UserProfileCreateRequest request)
        {
            var userProfile = new UserProfile(ownerId.ToString(), request);

            return await _repository.Create(userProfile);
        }

        public async Task<UserProfile> Update(Guid id, UserProfileUpdateRequest request)
        {
            var userProfile = await _repository.GetById(id.ToString());
            
            if (userProfile == null)
                throw new NotFoundException(id);

            if (!string.IsNullOrEmpty(request.Biography))
            {
                userProfile.Biography = request.Biography;
            }

            if (!string.IsNullOrEmpty(request.AvatarUrl))
            {
                userProfile.AvatarUrl = request.AvatarUrl;
            }

            return await _repository.Update(id.ToString(), userProfile);
        }

        public async Task Delete(Guid id, bool hardDelete = false)
        {
            await _repository.Delete(id.ToString(), hardDelete);
        }
    }
}