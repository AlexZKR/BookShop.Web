using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers;

public class HomeController : Controller
{

    public async Task<IActionResult> Index()
    {

        return View();
    }
}