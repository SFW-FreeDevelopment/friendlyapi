using System;
using System.Threading.Tasks;
using FriendlyApi.Service.Interfaces;
using FriendlyApi.Service.Models;

namespace FriendlyApi.Service.Services
{
    public class AuthService
    {
        private readonly IMongoRepository<User> _repository;

        public AuthService(IMongoRepository<User> repository)
        {
            _repository = repository;
        }
        
        public async Task<bool> Authenticate(string id, string password)
        {
            try
            {
                var user = await _repository.GetById(id);
                var isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
                return isValid;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}