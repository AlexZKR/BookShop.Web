using BookShop.Web.Interfaces;
using BookShop.Web.Services;

namespace BookShop.Web.Configuration;

public static class ConfigureWebServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBasketViewModelService, BasketViewModelService>();
        services.AddTransient<ICatalogViewModelService, CatalogViewModelService>();

        return services;
    }
}