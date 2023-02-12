using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers;

public class OrdersController : Controller
{
    public OrdersController()
    {

    }

    public IActionResult Index()
    {
        return View();
    }
}