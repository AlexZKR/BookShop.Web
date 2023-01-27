using BookShop.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.DAL;

public static class DbConfiguration
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        bool UseSqlite = false;
        if (configuration["DbConfig:UseSqlite"] != null)
        {
            UseSqlite = bool.Parse(configuration["DbConfig:UseSqlite"]!);
        }
        if (UseSqlite)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("sqliteAppDbContext")));

            services.AddDbContext<appIdentityDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("sqliteIdentityDbContext")));
        }
        else
        {
            //implement real db
        }
    }
}