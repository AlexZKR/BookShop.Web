using BookShop.BLL;
using BookShop.BLL.Interfaces;
using BookShop.BLL.Services;
using BookShop.DAL.Data;
using BookShop.DAL.Data.Queries;
using BookShop.DAL.Logging;
using BookShop.DAL.Services;

namespace BookShop.Web.Configuration;

public static class ConfigureBLLServices
{
    public static IServiceCollection AddBLLServices(this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddScoped(typeof(IReadRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IBookCatalogService, BookCatalogService>();

        services.AddTransient(typeof(IFavouriteService<>), typeof(FavouriteService<>));
        services.AddTransient<IRatingService, RatingService>();

        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<IBasketQueryService, BasketQueryService>();
        services.AddScoped<IOrderService, OrderService>();

        services.AddSingleton<IUriComposer>(new UriComposer(configuration.Get<CatalogSettings>()!));
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));


        return services;
    }
}