using System;
namespace Lafarage.Domain.Entities;

public class Positions
{
	[Key]
	public long Id { get; set; }
	public string CreatedAt { get; set; }
	public string UpdatedAt { get; set; }
	public int AgeOfReadingSeconds { get; set; }
	public long AssetId { get; set; }
	public string AltitudeMetres { get; set; }
	public long DriverId { get; set; }
	public string Heading { get; set; }
	public string Latitude { get; set; }
	public bool IsAvl { get; set; }
	public int OdometerKilometres { get; set; }
	public string Longitude { get; set; }
	public bool Hdop { get; set; }
	public bool PositionId { get; set; }
	public int Pdop { get; set; }
	public string TimeStamp { get; set; }
	public int Source { get; set; }
	public int SpeedKilometresPerHour { get; set; }
	public int SpeedLimit { get; set; }
	public int Vdop { get; set; }
}

