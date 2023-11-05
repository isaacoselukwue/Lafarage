global using Lafarage.Domain.Dtos.DataTransferObjects;
global using Lafarage.Service.Services.Interfaces;
global using Lafarage.Domain.Entities;

namespace Lafarage.Api.Controllers.v1;

public class LafarageController:BaseController
{
	private readonly ILafarageService lafarageService;
	public LafarageController(ILafarageService lafarageService)
	{
		this.lafarageService = lafarageService;
	}
	[HttpGet("GetAllDrivers")]
	public async Task<ActionResult<Result<List<Drivers>>>> GetAllDrivers()
	{
		Result<List<Drivers>> result = new()
		{
			RequestTime = GetCurrentServerTime()
		};
		result = await lafarageService.GetAllDrivers();
        result.ResponseTime = GetCurrentServerTime();
        return Ok(result);
    }
    [HttpGet("GetAllLocations")]
    public async Task<ActionResult<Result<List<GetAllLocationResponse>>>> GetAllLocations()
    {
        Result<List<GetAllLocationResponse>> result = new()
        {
            RequestTime = GetCurrentServerTime()
        };
        result = await lafarageService.GetAllLocations();
        result.ResponseTime = GetCurrentServerTime();
        return Ok(result);
    }
    [HttpGet("GetAllTrucks")]
    public async Task<ActionResult<Result<List<Trucks>>>> GetAllTrucks()
    {
        Result<List<Trucks>> result = new()
        {
            RequestTime = GetCurrentServerTime()
        };
        result = await lafarageService.GetAllTrucks();
        result.ResponseTime = GetCurrentServerTime();
        return Ok(result);
    }
    [HttpPost("GetTruckLocation")]
	public async Task<ActionResult<Result<GetTruckLocationResponse>>> GetTruckLocation([FromBody] GetTruckLocationRequest request)
	{
		Result<List<GetTruckLocationResponse>> result = new()
		{
			RequestTime = GetCurrentServerTime()
		};
		result = await lafarageService.GetTruckLocation(request);
		result.ResponseTime = GetCurrentServerTime();
		return Ok(result);
	}
    [HttpPost("GetStaticDistance")]
    public async Task<ActionResult<Result<string>>> GetStaticDistance([FromBody] GetDistanceFromLocationRequest request)
    {
        Result<string> result = new()
        {
            RequestTime = GetCurrentServerTime()
        };
        result = await lafarageService.GetStaticDistance(request);
        result.ResponseTime = GetCurrentServerTime();
        return Ok(result);
    }
}

