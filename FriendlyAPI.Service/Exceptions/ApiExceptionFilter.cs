using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using FriendlyApi.Service.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FriendlyApi.Service.Exceptions
{
    public class ApiExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;
        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is not ApiException exception) return;
            
            context.Result = new ObjectResult(
                new ErrorResponse
                {
                    Message = exception.Message
                })
            {
                StatusCode = exception.StatusCode,
            };
            context.ExceptionHandled = true;
        }
    }
}