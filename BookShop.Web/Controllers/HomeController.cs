using BookShop.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers;

public class HomeController : Controller
{

    public async Task<IActionResult> Index()
    {
        var vm = new CatalogViewModel();
        return View(vm);
    }
}