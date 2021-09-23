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
    [Route("users/{userId:guid}/userProfile")]
    public class UserProfileController : ControllerBase
    {
        private readonly UserProfileService _service;
        
        public UserProfileController(UserProfileService service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(UserProfile))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, typeof(ErrorResponse))]
        public async Task<IActionResult> GetById(Guid userId)
        {
            var resource = await _service.GetById(userId);
            return resource != null ? Ok(resource) : NotFound();
        }
        
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(UserProfile))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "", typeof(ErrorResponse))]
        public async Task<IActionResult> Create(Guid userId, UserProfileCreateRequest request)
        {
            var resource = await _service.Create(userId, request);
            return resource != null ? Created($"/users/{userId}/usersProfile", request) : UnprocessableEntity();
        }

        [HttpPut]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(UserProfile))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "", typeof(ErrorResponse))]
        public async Task<IActionResult> Update(Guid userId, UserProfileUpdateRequest request)
        {
            var resource = await _service.Update(userId, request);
            return resource != null ? Ok(resource) : NotFound();
        }

        [HttpDelete]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(UserProfile))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "", typeof(ErrorResponse))]
        public async Task<IActionResult> Delete(Guid userId, [FromQuery]bool hardDelete = false)
        {
            await _service.Delete(userId, hardDelete);
            return NoContent();
        }
    }
}