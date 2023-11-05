using System;
namespace Lafarage.Domain.Dtos.DataTransferObjects;

public class GetTruckLocationResponse
{
	public string Address { get; set; }
	public string Longitude { get; set; }
	public string Latitude { get; set; }
	public string Timestamp { get; set; }
}

