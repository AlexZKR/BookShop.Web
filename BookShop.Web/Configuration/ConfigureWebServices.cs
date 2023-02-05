using BookShop.Web.Interfaces;
using BookShop.Web.Services;

namespace BookShop.Web.Configuration;

public static class ConfigureWebServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBasketViewModelService, BasketViewModelService>();
        services.AddScoped<ICheckOutViewModelService, CheckOutViewModelService>();
        services.AddScoped<IOrderViewModelService, OrderViewModelService>();
        services.AddTransient<ICatalogViewModelService, CatalogViewModelService>();
        services.AddTransient<IProductDetailsViewModelService, ProductDetailsViewModelService>();

        return services;
    }
}