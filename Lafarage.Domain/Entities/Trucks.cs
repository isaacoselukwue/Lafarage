global using System.ComponentModel.DataAnnotations;

namespace Lafarage.Domain.Entities;

public class Trucks
{
	[Key]
	public long Id { get; set; }
	public string CreatedAt { get; set; }
	public string UpdatedAt { get; set; }
	public long SiteId { get; set; }
	public long AssetId { get; set; }
	public string RegistrationNumber { get; set; }
	public string LastPositionTimestamp { get; set; }
	public string Latitude { get; set; }
	public string Longitute { get; set; }
	public string CurrentAddress { get; set; }
}

