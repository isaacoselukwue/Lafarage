using System;
namespace Lafarage.Domain.Dtos.DataTransferObjects;

public class GetAllLocationResponse
{
    public long TruckId { get; set; }
    public long DriverId { get; set; }
    public string TruckRegistrationNumber { get; set; }
    public string DriverName { get; set; }
    public string Address { get; set; }
    public string Longitude { get; set; }
    public string Latitude { get; set; }
    public string TimeStamp { get; set; }
}

