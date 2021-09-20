using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FriendlyApi.Service.Exceptions;
using FriendlyApi.Service.Interfaces;
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
        
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(IEnumerable<User>))]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _service.GetAll();
        }
        
        [HttpGet]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(User))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "", typeof(ErrorResponse))]
        public async Task<User> GetById(Guid id)
        {
            return await _service.GetById(id);
        }
        
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(User))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "", typeof(ErrorResponse))]
        public async Task<User> Create(UserCreateRequest request)
        {
            return await _service.Create(request);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(User))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "", typeof(ErrorResponse))]
        public async Task<User> Update(Guid id, UserUpdateRequest request)
        {
            return await _service.Update(id, request);
        }

        [HttpDelete]
        [Route("{id:guid}")]
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