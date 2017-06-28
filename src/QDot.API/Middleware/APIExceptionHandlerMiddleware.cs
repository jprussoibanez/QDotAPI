using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using Newtonsoft.Json;
using QDot.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace QDot.API.Middleware
{
    public class APIExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public APIExceptionHandlerMiddleware(RequestDelegate next, ILogger<APIExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var errorMessage = "";

            if (exception is ServiceParameterException)
            {
                code = HttpStatusCode.BadRequest;
                errorMessage = exception.Message;
            } else
            {
                //TODO: Add text message to resource file
                _logger.LogError(0, exception, "An unhandled exception has occurred: " + exception.Message);
                code = HttpStatusCode.InternalServerError;
                //TODO: Add text message to resource file
                errorMessage = "Internal server error. Please contact administrator";
            }

            //TODO: Improve creation of the error message through something similar to CreateErrorResponse on System.Net.Http.HttpRequestMessageExtensions
            var result = JsonConvert.SerializeObject(new
            {
                error = errorMessage
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);

        }
    }
}