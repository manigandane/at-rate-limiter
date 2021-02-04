using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using rate_limiter.Factory;
using rate_limiter.Strategies;

namespace rate_limiter.Middlewares
{
    public class RateLimiterMiddleware
    {
        private const int MAXIMUM_REQUESTS = 100;
        private const int WINDOW_MINUTES = 60;

        private readonly RequestDelegate _next; 
        private readonly ILogger _logger;
        private readonly StrategyBase rateLimiter;
        public RateLimiterMiddleware(RequestDelegate next, ILoggerFactory logFactory)
        {
            _next = next;
            _logger = logFactory.CreateLogger("RateLimiter");
            rateLimiter = RateLimiterFactory.GetFixedWindowRateLimiterStrategy(MAXIMUM_REQUESTS, WINDOW_MINUTES);
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            _logger.LogInformation("Checking RateLimiter");
            var requestContext = new Models.RequestContext(Models.ContextKey.IpAddress, httpContext.Connection.RemoteIpAddress.ToString());
            var response = rateLimiter.ValidateRequest(new Models.RlRequest(requestContext, DateTime.UtcNow));

            if(response.Allowed)
            {
                await _next(httpContext);
            } else
            {
                var errorMessage = JsonSerializer.Serialize(new { error = response.Message});
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                await httpContext.Response.WriteAsync(errorMessage);
            }
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
