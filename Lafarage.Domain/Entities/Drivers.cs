using System;
namespace Lafarage.Domain.Entities;

public class Drivers
{
	[Key]
	public long Id { get; set; }
	public string CreatedAt { get; set; }
	public string UpdatedAt { get; set; }
	public long SiteId { get; set; }
	public long DriverId { get; set; }
	public string Name { get; set; }
	public string EmployeeNumber { get; set; }
}

