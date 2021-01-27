using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuth
{
    public class GuardMiddleware
    {
        private readonly RequestDelegate _next;

        public GuardMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            StringValues token = string.Empty;
            if (httpContext != null)
                httpContext.Request.Headers.TryGetValue("Authorization", out token);

            if (token.Any())
            {
               var user = JwtAuthenticator.GetPrincipal(token.First());
               httpContext.User = user;                
            }
            await _next(httpContext).ConfigureAwait(true);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class BarrierMiddlewareExtensions
    {
        public static IApplicationBuilder UseGuardMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GuardMiddleware>();
        }
    }
}
