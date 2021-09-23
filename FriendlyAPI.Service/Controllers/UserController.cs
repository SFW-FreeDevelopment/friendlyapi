using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FriendlyApi.Service.Models;
using FriendlyApi.Service.Models.Requests;
using FriendlyApi.Service.Models.Responses;
using FriendlyApi.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FriendlyApi.Service.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        
        public UserController(UserService service)
        {
            _service = service;
        }
        
        [Obsolete]
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(IEnumerable<User>))]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }
        
        [HttpGet("search")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(IEnumerable<User>))]
        public async Task<IActionResult> Search()
        {
            throw new NotImplementedException();
        }
        
        [HttpGet("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(User))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, typeof(ErrorResponse))]
        public async Task<IActionResult> GetById(Guid id)
        {
            var resource = await _service.GetById(id);
            return resource != null ? Ok(resource) : NotFound();
        }
        
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(User))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "", typeof(ErrorResponse))]
        public async Task<IActionResult> Create(UserCreateRequest request)
        {
            var resource = await _service.Create(request);
            return resource != null ? Created($"/users/{resource.Id}", request) : UnprocessableEntity();
        }

        [HttpPut("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(User))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "", typeof(ErrorResponse))]
        public async Task<IActionResult> Update(Guid id, UserUpdateRequest request)
        {
            var resource = await _service.Update(id, request);
            return resource != null ? Ok(resource) : NotFound();
        }

        [HttpDelete("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(User))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "", typeof(ErrorResponse))]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}