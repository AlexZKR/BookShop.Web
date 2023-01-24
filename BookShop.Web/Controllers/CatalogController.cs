using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers;

public class CatalogController : Controller
{
    public async Task<IActionResult> Index()
    {
        return View();
    }
}