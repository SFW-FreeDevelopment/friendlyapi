using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FriendlyApi.Service.Exceptions;
using FriendlyApi.Service.Models;
using FriendlyApi.Service.Models.Requests;
using FriendlyApi.Service.Repositories.Interfaces;

namespace FriendlyApi.Service.Services
{
    public class ProfileService
    {
        private readonly IMongoRepository<Profile> _repository;

        public ProfileService(IMongoRepository<Profile> repository)
        {
            _repository = repository;
        }
        
        public async Task<IEnumerable<Profile>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Profile> GetById(Guid id)
        {
            var profile = await _repository.GetById(id.ToString());
            
            if (profile == null)
                throw new NotFoundException(id);
            
            return profile;
        }

        public async Task<Profile> Create(Guid ownerId, ProfileCreateRequest request)
        {
            var profile = new Profile(ownerId.ToString(), request);

            return await _repository.Create(profile);
        }

        public async Task<Profile> Update(Guid id, ProfileUpdateRequest request)
        {
            var profile = await _repository.GetById(id.ToString());
            
            if (profile == null)
                throw new NotFoundException(id);

            if (!string.IsNullOrEmpty(request.Biography))
            {
                profile.Biography = request.Biography;
            }

            if (!string.IsNullOrEmpty(request.AvatarUrl))
            {
                profile.AvatarUrl = request.AvatarUrl;
            }

            return await _repository.Update(id.ToString(), profile);
        }

        public async Task Delete(Guid id, bool hardDelete = false)
        {
            await _repository.Delete(id.ToString(), hardDelete);
        }
    }
}