using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QDot.API.Middleware
{
    public static class QDotAPIMiddlewareExtensions
    {
        public static IApplicationBuilder UseAPIExceptionHandler(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<APIExceptionHandlerMiddleware>();
        }
    }
}
