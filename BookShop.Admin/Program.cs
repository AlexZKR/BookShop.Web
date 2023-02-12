using BookShop.Admin;
using BookShop.Admin.Configuration;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
SD.MainAPIBase = builder.Configuration["ServiceUrls:MainAPI"];

builder.Services.AddServices();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Orders}/{action=Index}/{id?}");
app.MapControllers();

app.Run();
