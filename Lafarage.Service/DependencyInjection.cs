global using Lafarage.Service.Services.Implementations;

namespace Lafarage.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ILafarageService, LafarageService>();
        return services;
    }
}

