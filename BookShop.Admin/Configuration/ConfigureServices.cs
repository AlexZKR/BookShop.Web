using AutoMapper;
using BookShop.Admin.Configuration.MapperConfig;
using BookShop.Admin.Interfaces;
using BookShop.Admin.Services;

namespace BookShop.Admin.Configuration;

public static class ConfigureServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddHttpClient<IOrderService, OrderService>();
        services.AddScoped<IOrderService, OrderService>();

        //automapper config
        var mapperConfig = new MapperConfiguration(mc => {
            mc.AddProfile(new DTOMapProfile());
        });
        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}