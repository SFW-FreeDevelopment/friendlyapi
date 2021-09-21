using Microsoft.AspNetCore.Http;

namespace FriendlyApi.Service.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string message = null)
            : base(message ?? "Resource could not be found",StatusCodes.Status404NotFound) { }
        
        public NotFoundException(object id) : this($"Resource of ID {id} could not be found") { }
    }
}