using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers;

public class ProductController : Controller
{
    public async Task<IActionResult> Index(int id)
    {
        return View();
    }
}