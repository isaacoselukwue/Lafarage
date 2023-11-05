global using Lafarage.Domain.Dtos.DataTransferObjects;
global using Lafarage.Domain.Entities;

namespace Lafarage.Service.Services.Interfaces;

public interface ILafarageService
{
    Task<Result<List<Drivers>>> GetAllDrivers();
    Task<Result<List<GetAllLocationResponse>>> GetAllLocations();
    Task<Result<List<Trucks>>> GetAllTrucks();
    Task<Result<string>> GetStaticDistance(GetDistanceFromLocationRequest request);
    Task<Result<List<GetTruckLocationResponse>>> GetTruckLocation(GetTruckLocationRequest request);
}

