using BookShop.Web.Extensions;
using BookShop.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers;

public class HomeController : Controller
{
    private readonly ICatalogViewModelService catalogViewModelService;

    public HomeController(ICatalogViewModelService catalogViewModelService)
    {
        this.catalogViewModelService = catalogViewModelService;
    }

    public async Task<IActionResult> Index()
    {
        var catalogModel = await catalogViewModelService
        .GetTopSoldItemsViewModel(3, HttpContext.GetUsername());

        return View(catalogModel);
    }
}