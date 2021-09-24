using System;
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
    [Route("users/{id:guid}/profile")]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService _service;
        
        public ProfileController(ProfileService service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(Profile))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, typeof(ErrorResponse))]
        public async Task<IActionResult> GetById(Guid id)
        {
            var resource = await _service.GetById(id);
            return resource != null ? Ok(resource) : NotFound();
        }
        
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(Profile))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "", typeof(ErrorResponse))]
        public async Task<IActionResult> Create(Guid id, ProfileCreateRequest request)
        {
            var resource = await _service.Create(id, request);
            return resource != null ? Created($"/users/{id}/usersProfile", request) : UnprocessableEntity();
        }

        [HttpPut]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(Profile))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "", typeof(ErrorResponse))]
        public async Task<IActionResult> Update(Guid id, ProfileUpdateRequest request)
        {
            var resource = await _service.Update(id, request);
            return resource != null ? Ok(resource) : NotFound();
        }

        [HttpDelete]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(Profile))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "", typeof(ErrorResponse))]
        public async Task<IActionResult> Delete(Guid id, [FromQuery]bool hardDelete = false)
        {
            await _service.Delete(id, hardDelete);
            return NoContent();
        }
    }
}