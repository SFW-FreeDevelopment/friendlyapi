using System.Threading.Tasks;
using FriendlyApi.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FriendlyApi.Service.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;
        
        public AuthController(AuthService service)
        {
            _service = service;
        }
        
        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> Authenticate([FromRoute] string id, [FromBody] string password)
        {
            var isValid = await _service.Authenticate(id, password);
            return isValid;
        }
    }
}