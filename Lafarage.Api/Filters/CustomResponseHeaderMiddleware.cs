using System;
namespace Lafarage.Api.Filters;

public class CustomResponseHeaderMiddleware
{
    private readonly RequestDelegate next;
    public CustomResponseHeaderMiddleware(RequestDelegate next)
    {
        this.next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        context.Response.OnStarting(state =>
        {
            var httpContext = (HttpContext)state;
            httpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            httpContext.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
            httpContext.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
            httpContext.Response.Headers.Add("Content-Security-Policy", "default-src 'self'");
            httpContext.Response.Headers.Remove("X-Powered-By");
            httpContext.Response.Headers.Remove("Server");
            httpContext.Response.Headers.Remove("server");
            httpContext.Response.Headers.Remove("x-aspnet-version");
            return Task.CompletedTask;
        }, context);
        await next(context);
    }
}