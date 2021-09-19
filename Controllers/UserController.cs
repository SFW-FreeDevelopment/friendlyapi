using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FriendlyApi.Service.Interfaces;
using FriendlyApi.Service.Models;
using FriendlyApi.Service.Models.Requests;
using FriendlyApi.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

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
        
        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _service.GetAll();
        }
        
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<User> GetById(Guid id)
        {
            return await _service.GetById(id);
        }
        
        [HttpPost]
        public async Task<User> Create(UserCreateRequest request)
        {
            return await _service.Create(request);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<User> Update(Guid id, UserUpdateRequest request)
        {
            return await _service.Update(id, request);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status204NoContent, null)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}