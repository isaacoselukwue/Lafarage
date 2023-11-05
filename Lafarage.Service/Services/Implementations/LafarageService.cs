global using Lafarage.Data.Repositories.Interfaces;
global using Lafarage.Service.Services.Interfaces;
global using Lafarage.Domain.Common.Generics;
global using Serilog;
global using System.Text.Json;
using Lafarage.Domain.Entities;

namespace Lafarage.Service.Services.Implementations;

public class LafarageService : ILafarageService
{
	private readonly ILafarageRepository lafarageRepository; 
	private readonly ILogger logger;
	public LafarageService(ILafarageRepository lafarageRepository, ILogger logger)
	{
		this.lafarageRepository = lafarageRepository;
		this.logger = logger;
	}
	public async Task<Result<List<GetTruckLocationResponse>>> GetTruckLocation(GetTruckLocationRequest request)
	{
		Result<List<GetTruckLocationResponse>> result = new()
		{
			Content = new(),
			IsSuccess = false,
			RequestTime = DateTime.UtcNow.AddHours(1)
		};
		logger.Information($"Method: {nameof(GetTruckLocation)}. Request: {JsonSerializer.Serialize(request)}");
		result.Content = await lafarageRepository.GetTruckLocation(request);
        logger.Information($"Method: {nameof(GetTruckLocation)}. Response: {JsonSerializer.Serialize(result.Content)}");
		if (result.Content.Any())
		{
			result.Message = "Successfully retrieved trucks location";
			result.IsSuccess = true;
		}
		else result.Message = "No data retrieved";
		return result;
	}
    public async Task<Result<List<Drivers>>> GetAllDrivers()
    {
        Result<List<Drivers>> result = new()
        {
            Content = new(),
            IsSuccess = false,
            RequestTime = DateTime.UtcNow.AddHours(1)
        };
        result.Content = await lafarageRepository.GetAllDrivers();
        logger.Information($"Method: {nameof(GetAllDrivers)}. Response: {JsonSerializer.Serialize(result.Content)}");
        if (result.Content.Any())
        {
            result.Message = "Successfully retrieved drivers information";
            result.IsSuccess = true;
        }
        else result.Message = "No data retrieved";
        return result;
    }
    public async Task<Result<List<GetAllLocationResponse>>> GetAllLocations()
    {
        Result<List<GetAllLocationResponse>> result = new()
        {
            Content = new(),
            IsSuccess = false,
            RequestTime = DateTime.UtcNow.AddHours(1)
        };
        result.Content = await lafarageRepository.GetAllLocations();
        logger.Information($"Method: {nameof(GetAllLocations)}. Response: {JsonSerializer.Serialize(result.Content)}");
        if (result.Content.Any())
        {
            result.Message = "Successfully retrieved all information";
            result.IsSuccess = true;
        }
        else result.Message = "No data retrieved";
        return result;
    }
    public async Task<Result<List<Trucks>>> GetAllTrucks()
    {
        Result<List<Trucks>> result = new()
        {
            Content = new(),
            IsSuccess = false,
            RequestTime = DateTime.UtcNow.AddHours(1)
        };
        result.Content = await lafarageRepository.GetAllTrucks();
        logger.Information($"Method: {nameof(GetAllTrucks)}. Response: {JsonSerializer.Serialize(result.Content)}");
        if (result.Content.Any())
        {
            result.Message = "Successfully retrieved trucks information";
            result.IsSuccess = true;
        }
        else result.Message = "No data retrieved";
        return result;
    }
    public async Task<Result<string>> GetStaticDistance(GetDistanceFromLocationRequest request)
    {
        Result<string> result = new()
        {
            IsSuccess = false,
            RequestTime = DateTime.UtcNow.AddHours(1)
        };
        logger.Information($"Method: {nameof(GetStaticDistance)}. Request: {JsonSerializer.Serialize(request)}");
        Trucks? trucks = await lafarageRepository.GetATruckByAssetId(request.AssetId);
        if (trucks is null)
        {
            result.Error = new Domain.Common.Error
            {
                Code = 400,
                Message = "Asset not found",
                Type = "Not Found"
            };
            result.ErrorMessage = "Asset not found!";
            result.Message = "Asset not found!";
            return result;
        }
        double trucksLongitude = Convert.ToDouble(trucks.Longitute);
        double truckLatitude = Convert.ToDouble(trucks.Latitude);
        double staticLatitude = Convert.ToDouble(request.StaticLatitude);
        double staticLongitude = Convert.ToDouble(request.StaticLongitude);
        double distanceInKm = CalculateDistance(truckLatitude, trucksLongitude, staticLatitude, staticLongitude);
        result.Message = $"Distance from location is: {distanceInKm}";
        result.IsSuccess = true;
        return result;
    }
    private static double CalculateDistance(double truckLatitude, double truckLongitude, double staticLatitude, double staticLongitude)
    {
        double truckLatitudeRadians = truckLatitude * Math.PI / 180;
        double truckLongitudeRadians = truckLongitude * Math.PI / 180;
        double staticLatitudeRadians = staticLatitude * Math.PI / 180;
        double staticLongitudeRadians = staticLongitude * Math.PI / 180;

        double deltaLatitude = staticLatitudeRadians - truckLatitudeRadians;
        double deltaLongitude = staticLongitudeRadians - truckLongitudeRadians;

        double a = Math.Sin(deltaLatitude / 2) * Math.Sin(deltaLatitude / 2) +
            Math.Cos(truckLatitudeRadians) * Math.Cos(staticLatitudeRadians) *
            Math.Sin(deltaLongitude / 2) * Math.Sin(deltaLongitude / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        double distance = 6371 * c;

        return distance;
    }

}

