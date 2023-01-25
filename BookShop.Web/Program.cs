using BookShop.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BookShop.DAL.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("sqliteAppDbContext"))
);

builder.Services.AddDbContext<appIdentityDbContext>(
    options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("sqliteIdentityDbContext")));


builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
.AddEntityFrameworkStores<appIdentityDbContext>();



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
