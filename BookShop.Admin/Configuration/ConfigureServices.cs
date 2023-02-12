using BookShop.Admin.Interfaces;
using BookShop.Admin.Services;

namespace BookShop.Admin.Configuration;

public static class ConfigureServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddHttpClient<IEntityService, EntityService>();
        services.AddScoped<IEntityService, EntityService>();


        return services;
    }
}