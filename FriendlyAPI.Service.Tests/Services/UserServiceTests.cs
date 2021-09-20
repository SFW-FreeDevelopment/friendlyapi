using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendlyApi.Service.Interfaces;
using FriendlyApi.Service.Models;
using FriendlyApi.Service.Services;
using Moq;
using Xunit;

namespace FriendlyAPI.Service.Tests.Services
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly Mock<IMongoRepository<User>> _repository = new Mock<IMongoRepository<User>>();

        private readonly IList<User> _users = new List<User>();
        private readonly User _user1 = new User { Id = Guid.NewGuid().ToString() };
        private readonly User _user2 = new User { Id = Guid.NewGuid().ToString() };
        
        public UserServiceTests()
        {
            _users.Add(_user1);
            _users.Add(_user2);

            _repository.Setup(r => r.GetAll()).ReturnsAsync(_users);
            _repository.Setup(r => r.GetById(_user1.Id)).ReturnsAsync(_user1);
            _repository.Setup(r => r.GetById(_user2.Id)).ReturnsAsync(_user2);
            
            _userService = new UserService(_repository.Object);
        }
        
        #region GetAll
        [Fact]
        public async Task GetAll_Returns_IEnumerableOfUser_WhenSuccessful()
        {
            var users = await _userService.GetAll();
            
            Assert.NotNull(users);
            Assert.NotEmpty(users);
            Assert.Contains(_user1, users);
            Assert.Contains(_user2, users);
        }

        [Fact]
        public async Task GetAll_Throws_Exception_WhenNotSuccessful()
        {
            const string errorMessage = "Something went wrong";
            _repository.Setup(r => r.GetAll()).ThrowsAsync(new Exception(errorMessage));
            Exception exception = null;
            try
            {
                await _userService.GetAll();
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.NotNull(exception);
            Assert.Equal(errorMessage, exception.Message);
        }
        #endregion
    }
}