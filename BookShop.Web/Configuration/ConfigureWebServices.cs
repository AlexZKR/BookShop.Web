using BookShop.Web.Interfaces;
using BookShop.Web.Services;

namespace BookShop.Web.Configuration;

public static class ConfigureWebServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IBasketViewModelService, BasketViewModelService>();
        services.AddTransient<ICheckOutViewModelService, CheckOutViewModelService>();
        services.AddTransient<IOrderViewModelService, OrderViewModelService>();
        services.AddTransient<ICatalogViewModelService, BookCatalogViewModelService>();
        services.AddTransient<IProductDetailsViewModelService, ProductDetailsViewModelService>();
        services.AddTransient<IRatingViewModelService, RatingViewModelService>();

        return services;
    }
}