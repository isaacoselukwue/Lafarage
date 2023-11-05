global using Lafarage.Domain.Common.Generics;
global using Microsoft.Extensions.Caching.Memory;

namespace Lafarage.Api.Filters;

public class RateLimitingMiddleware
{
    private readonly RequestDelegate next;
    private readonly IMemoryCache cache;
    private const string CacheKeyPrefix = "RateLimit_";
    private const int RequestLimit = 20;
    private const int ResetSeconds = 60;
    public RateLimitingMiddleware(RequestDelegate next, IMemoryCache cache)
    {
        this.next = next;
        this.cache = cache;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();
        var cacheKey = CacheKeyPrefix + ipAddress;
        if (!cache.TryGetValue(cacheKey, out int requestCount))
        {
            requestCount = 0;
        }
        if (requestCount >= RequestLimit)
        {
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new Result<string>
            {
                IsSuccess = false,
                RequestTime = DateTime.UtcNow.AddHours(1),
                Message = "😔 We have received too many requests from you, please try later.",
                ResponseTime = DateTime.UtcNow.AddHours(1),
                ErrorMessage = "Too many requests",
                Error = new Domain.Common.Error
                {
                    Code = 500,
                    Message = "Too many requests",
                    Type = "Authorization violation"
                },
                Content = "😔 We have received too many requests from you, please try later.",
                RequestId = Guid.NewGuid().ToString()
            });
            return;
        }
        cache.Set(cacheKey, requestCount + 1, TimeSpan.FromSeconds(ResetSeconds));
        await next(context);
    }
}