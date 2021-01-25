using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace rate_limiter.Middlewares
{
    public class RateLimiterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public RateLimiterMiddleware(RequestDelegate next, ILoggerFactory logFactory)
        {
            _next = next;
            _logger = logFactory.CreateLogger("RateLimiter");
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            _logger.LogInformation("Checking RateLimiter");
            await _next(httpContext);
        }
    }

    // Extension method for adding the middleware to the HTTP request pipeline.
    public static class RateLimiterMiddlewareExtensions
    {
        public static IApplicationBuilder UseRateLimiterMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RateLimiterMiddleware>();
        }
    }
}
