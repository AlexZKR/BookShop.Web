using AutoMapper;
using BookShop.Web.Configuration.MapperConfig;
using BookShop.Web.Interfaces;
using BookShop.Web.Services;

namespace BookShop.Web.Configuration;

public static class ConfigureWebServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
        //viewmodel services
        services.AddTransient<IBasketViewModelService, BasketViewModelService>();
        services.AddTransient<ICheckOutViewModelService, CheckOutViewModelService>();
        services.AddTransient<IOrderViewModelService, OrderViewModelService>();
        services.AddTransient<ICatalogViewModelService, BookCatalogViewModelService>();
        services.AddTransient<IProductDetailsViewModelService, ProductDetailsViewModelService>();
        services.AddTransient<IRatingViewModelService, RatingViewModelService>();
        

        //automapper config
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new DTOMapProfile());
            mc.AddProfile(new ViewModelMapProfile());
        });
        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);


        return services;
    }
}