using BookShop.Admin;
using BookShop.Admin.Configuration;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
SD.MainAPIBase = builder.Configuration["ServiceUrls:MainAPI"];

builder.Services.AddServices();

var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(@"E:\BSUIR\Diploma\Zakrevsky Diploma\BookShop.Web\wwwroot","img","books")),
    RequestPath = "/Books"
});

app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");
app.MapControllers();

app.Run();
