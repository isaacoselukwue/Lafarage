global using Lafarage.Domain.Entities;
global using Microsoft.EntityFrameworkCore;

namespace Lafarage.Data.Configuration.Implementations;

public class LafarageDbContext : DbContext
{
	public LafarageDbContext(DbContextOptions<LafarageDbContext> options) : base(options)
    {
	}
	public DbSet<Drivers> Drivers { get; set; }
	public DbSet<Positions> Positions { get; set; }
	public DbSet<Trucks> Trucks { get; set; }
}

