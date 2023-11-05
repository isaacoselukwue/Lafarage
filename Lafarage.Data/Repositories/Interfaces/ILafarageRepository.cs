
namespace Lafarage.Data.Repositories.Interfaces;

public interface ILafarageRepository
{
    Task<List<Drivers>> GetAllDrivers();
    Task<List<GetAllLocationResponse>> GetAllLocations();
    Task<List<Trucks>> GetAllTrucks();
    Task<Trucks?> GetATruckByAssetId(long assetId);
    Task<List<GetTruckLocationResponse>> GetTruckLocation(GetTruckLocationRequest request);
}

