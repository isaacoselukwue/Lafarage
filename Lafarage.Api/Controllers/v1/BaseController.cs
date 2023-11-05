global using Microsoft.AspNetCore.Mvc;

namespace Lafarage.Api.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class BaseController : ControllerBase
{
    public BaseController()
    {
    }
    internal static DateTime GetCurrentServerTime()
    {
        return DateTime.UtcNow.AddHours(1);
    }
}

