using Microsoft.AspNetCore.Http;

namespace FriendlyApi.Service.Exceptions
{
    public class InvalidRequestException : ApiException
    {
        public InvalidRequestException(string message = null)
            : base(message ?? "The request cannot be processed.", StatusCodes.Status422UnprocessableEntity) { }
        
        public InvalidRequestException(object id) : this($"The request cannot be processed for resource {id}.") {}
    }
}