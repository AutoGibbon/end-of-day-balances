using EndOfDayBalances.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace EndOfDayBalances.Middleware
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        private readonly ILogger<HttpResponseExceptionFilter> _logger;
        public int Order => int.MaxValue - 10;

        public HttpResponseExceptionFilter(ILogger<HttpResponseExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
            {
                return;
            }

            GenerateExceptionResponse(context);
            LogException(context);
        }

        private void GenerateExceptionResponse(ActionExecutedContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            if (context.Exception is HttpResponseException httpResponseException)
            {
                statusCode = httpResponseException.StatusCode;
            }

            context.Result = new ObjectResult(new ErrorResponse { Code = statusCode.ToString(), Description = context.Exception.Message })
            {
                StatusCode = (int)statusCode
            };

            context.ExceptionHandled = true;
        }

        private void LogException(ActionExecutedContext context)
        {
            _logger.LogError(context.Exception, "Error at {Path}, {Message}", context.HttpContext.Request.Path, context.Exception.Message);
        }
    }
}