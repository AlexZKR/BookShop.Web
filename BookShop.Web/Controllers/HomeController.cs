using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }
}