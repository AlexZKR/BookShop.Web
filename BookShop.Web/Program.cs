using BookShop.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BookShop.BLL.Entities;
using BookShop.BLL.Interfaces;
using BookShop.Web.Services;
using BookShop.Web.Interfaces;
using Ardalis.Specification;
using BookShop.DAL.Logging;
using BookShop.BLL;
using BookShop.BLL.Services;
using BookShop.DAL.Data.Queries;
using Microsoft.AspNetCore.Identity.UI.Services;
using BookShop.DAL.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("sqliteAppDbContext"))
);

builder.Services.AddDbContext<appIdentityDbContext>(
    options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("sqliteIdentityDbContext")));


// builder.Services.AddDefaultIdentity<IAppUser>(options => options.SignIn.RequireConfirmedAccount = true)
// .AddEntityFrameworkStores<appIdentityDbContext>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().
AddEntityFrameworkStores<appIdentityDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped(typeof(IReadRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


builder.Services.AddSingleton<IUriComposer>(new UriComposer(builder.Configuration.Get<CatalogSettings>()!));

builder.Services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

builder.Services.AddTransient(typeof(IFavouriteService<>), typeof(FavouriteService<>));
builder.Services.AddTransient<IBasketViewModelService, BasketViewModelService>();
builder.Services.AddScoped<IBasketQueryService, BasketQueryService>();
builder.Services.AddTransient<BookShop.BLL.Interfaces.IEmailSender, EmailSender>();

var app = builder.Build();

app.Logger.LogInformation("App created...");

app.Logger.LogInformation("Seeding Database...");

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var context = scopedProvider.GetRequiredService<AppDbContext>();
        await DbSeeder.SeedDataAsync(context, app.Logger);

        var idContext = scopedProvider.GetRequiredService<appIdentityDbContext>();
        await DbSeeder.SeedIdDataAsync(idContext, app.Logger);
    }
    catch (System.Exception)
    {

        throw;
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


app.Run();
