global using Lafarage.Data.Repositories.Interfaces;
global using Polly.Retry;
global using Lafarage.Data.Configuration.Implementations;
global using Polly;
global using Serilog;
global using Lafarage.Domain.Dtos.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Lafarage.Data.Repositories.Implementations;

public class LafarageRepository : ILafarageRepository
{
    private readonly AsyncRetryPolicy transientErrorRetryPolicy;
    private readonly LafarageDbContext context;
    public LafarageRepository(LafarageDbContext context)
	{
        this.context = context;
        this.transientErrorRetryPolicy = Policy.Handle<Exception>()
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
        onRetryAsync: (ex, count, context) =>
        {
            Log.Error(ex, $"Transaction failed Retrying due to {ex.GetType().Name}... Attempt {count}: {ex.Message}");
            return Task.CompletedTask;
        });
    }
    public async Task<List<GetTruckLocationResponse>> GetTruckLocation(GetTruckLocationRequest request)
    {
        List<GetTruckLocationResponse> getTruckLocationResponses = new();
        await transientErrorRetryPolicy.ExecuteAsync(async () =>
        {
            getTruckLocationResponses = await context.Positions
                .Where(x => x.AssetId == request.AssetId && x.DriverId == request.DriverId)
                .Join(context.Trucks.Where(x => x.AssetId == request.AssetId),
                x => x.AssetId,
                y => y.AssetId,
                (x, y) => new GetTruckLocationResponse
                {
                    Address = y.CurrentAddress,
                    Latitude = y.Latitude,
                    Longitude = y.Longitute,
                    Timestamp = y.LastPositionTimestamp
                }).AsNoTracking()
                .ToListAsync();
        });
        return getTruckLocationResponses;
    }
    public async Task<List<Drivers>> GetAllDrivers()
    {
        List<Drivers> drivers = new();
        await transientErrorRetryPolicy.ExecuteAsync(async () =>
        {
            drivers = await context.Drivers.AsNoTracking().ToListAsync();
        });
        return drivers;
    }
    public async Task<List<Trucks>> GetAllTrucks()
    {
        List<Trucks> trucks = new();
        await transientErrorRetryPolicy.ExecuteAsync(async () =>
        {
            trucks = await context.Trucks.AsNoTracking().ToListAsync();
        });
        return trucks;
    }
    public async Task<List<GetAllLocationResponse>> GetAllLocations()
    {
        List<GetAllLocationResponse> allLocationResponses = new();
        await transientErrorRetryPolicy.ExecuteAsync(async () =>
        {
            allLocationResponses = await context.Trucks
                .Join(context.Drivers,
                x => x.SiteId,
                y => y.SiteId,
                (x, y) => new GetAllLocationResponse
                {
                    Address = x.CurrentAddress,
                    DriverId = y.DriverId,
                    DriverName = y.Name,
                    Latitude = x.Latitude,
                    Longitude = x.Longitute,
                    TimeStamp = x.LastPositionTimestamp,
                    TruckId = x.AssetId,
                    TruckRegistrationNumber = x.RegistrationNumber
                })
                .AsNoTracking()
                .ToListAsync();
        });
        return allLocationResponses;
    }
    public async Task<Trucks?> GetATruckByAssetId(long assetId)
    {
        Trucks? trucks = new();
        await transientErrorRetryPolicy.ExecuteAsync(async () =>
        {
            trucks = await context.Trucks
                .Where(x => x.AssetId == assetId)
                .FirstOrDefaultAsync();
        });
        return trucks;
    }
}

