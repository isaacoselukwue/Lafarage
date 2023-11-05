using System;
namespace Lafarage.Domain.Dtos.DataTransferObjects;

public class GetTruckLocationRequest
{
	public long AssetId { get; set; }
	public long DriverId { get; set; }
}

