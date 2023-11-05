using System;
namespace Lafarage.Domain.Dtos.DataTransferObjects;

public class GetDistanceFromLocationRequest
{
	public long AssetId { get; set; }
	public string StaticLatitude { get; set; }
	public string StaticLongitude { get; set; }
}

