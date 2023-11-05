

namespace Lafarage.Api.Filters;

public partial class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;
    private readonly Serilog.ILogger logger;
    public GlobalExceptionHandlingMiddleware(RequestDelegate next, Serilog.ILogger logger)
    {
        this.next = next;
        this.logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            logger.Error(e, "Error");
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new Result<string>
            {
                IsSuccess = false,
                Error = new Domain.Common.Error
                {
                    Message = e.Message,
                    Code = 500,
                    Type = "System exception"
                },
                Content = "😞 An error occured processing request",
                ErrorMessage = "We could not process your request at this time",
                Message = "😔 System could not process your request at this time",
                RequestId = Guid.NewGuid().ToString(),
                RequestTime = DateTime.UtcNow.AddHours(1),
                ResponseTime = DateTime.UtcNow.AddHours(1)
            });
        }
    }
}