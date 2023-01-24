using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers;

public class InfoController : Controller
{
    public IActionResult About()
    {
        return View();
    }
    public IActionResult Contacts()
    {
        return View();
    }
}
