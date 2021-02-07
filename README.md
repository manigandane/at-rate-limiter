# Rate Limiter Module

Extendable rate limiting module for do net applications. This module is designed in a way that it can be used for various rate limiting staregies and the storage to be used for tracking requests are also customizable.

> **What's so great about this module:**

> - Module can be extended for different rate limiting strategies such as Leaky Bucket, Token Bucket etc
> - Incoming requests can be tracked using different storages such as in memory, redis or database
> - This also has the flexibility to use different middlewares

This repo has following three folders
- **at-core-api** : This project is a sample asp.net core api which uses the rate limiter through asp.net core middleware
- **rate-limiter** : This is the main module where all the rate limiting code is written
- **rate-limiter-tests** : xUnit test project for all unit tests

To extend this module for different storage options, new storage class should extend an interface IStorage

```
public interface IStorage
    {
        public bool StoreRequest(RlRequest request);
        public List<RlRequest> GetAllRequests();
        public bool DeleteRequest(RlRequest request);
    }
```

To add a new strategy, the strategy class should inherent from StrategyBase.

```
public abstract class StrategyBase
    {
        protected readonly IStorage storage;
        public StrategyBase(IStorage storage)
        {
            this.storage = storage;
        }

        public abstract RlResponse ValidateRequest(RlRequest request);
    }
```

Below example shows how this module can be used for asp.net core project
- First, Strategy object would need to be created for the required strategy with a storage option
```
const int MAXIMUM_REQUESTS = 100;
const int WINDOW_MINUTES = 60;
StrategyBase rateLimiter = RateLimiterFactory.GetFixedWindowRateLimiterStrategy(new MemoryStorage(), MAXIMUM_REQUESTS, WINDOW_MINUTES);
```

- Request can then be validated using ValidateRequest by passing the context
```
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
```
