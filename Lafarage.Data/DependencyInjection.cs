global using Lafarage.Data.Repositories.Implementations;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;

namespace Lafarage.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDataDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ILafarageRepository, LafarageRepository>();
        services.AddDbContext<LafarageDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("Default")));
        return services;
    }
}

