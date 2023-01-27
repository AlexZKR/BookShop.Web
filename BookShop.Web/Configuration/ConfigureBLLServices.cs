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

        services.AddSingleton<IUriComposer>(new UriComposer(configuration.Get<CatalogSettings>()!));
        services.AddScoped<IBasketQueryService, BasketQueryService>();
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));


        return services;
    }
}