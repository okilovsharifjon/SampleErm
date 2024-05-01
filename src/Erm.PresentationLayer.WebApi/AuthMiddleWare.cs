using System.Globalization;
using System.Net;

using Microsoft.Extensions.Primitives;

namespace Erm.PresentationLayer.WebApi
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        const string secretKey = "123";

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("KEY", out StringValues key) && key == secretKey)
            {
                await _next(context);
            }
            else
            {
                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsJsonAsync(new { Error = "Your are not authorized" });
            }
        }
    }
}