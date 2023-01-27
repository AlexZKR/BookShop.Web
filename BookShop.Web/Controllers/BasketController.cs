using Ardalis.GuardClauses;
using BookShop.BLL.Interfaces;
using BookShop.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookShop.BLL;


namespace BookShop.Web.Controllers;

public class BasketController : Controller
{
    private readonly IBasketViewModelService basketViewModelService;

    public BasketController(IBasketViewModelService basketViewModelService)
    {
        this.basketViewModelService = basketViewModelService;
    }
    public async Task<IActionResult> Index()
    {
        var vm = await basketViewModelService.GetOrCreateBasketForUser(GetOrSetBasketCookieAndUserName());
        return View(vm);
    }



    private string GetOrSetBasketCookieAndUserName()
    {
        Guard.Against.Null(Request.HttpContext.User.Identity, nameof(Request.HttpContext.User.Identity));
        string? userName = null;

        if (Request.HttpContext.User.Identity.IsAuthenticated)
        {
            Guard.Against.Null(Request.HttpContext.User.Identity.Name, nameof(Request.HttpContext.User.Identity.Name));
            return Request.HttpContext.User.Identity.Name!;
        }

        if (Request.Cookies.ContainsKey(SD.BASKET_COOKIENAME))
        {
            userName = Request.Cookies[SD.BASKET_COOKIENAME];

            if (!Request.HttpContext.User.Identity.IsAuthenticated)
            {
                if (!Guid.TryParse(userName, out var _))
                {
                    userName = null;
                }
            }
        }
        if (userName != null) return userName;

        userName = Guid.NewGuid().ToString();
        var cookieOptions = new CookieOptions { IsEssential = true };
        cookieOptions.Expires = DateTime.Today.AddYears(10);
        Response.Cookies.Append(SD.BASKET_COOKIENAME, userName, cookieOptions);

        return userName;
    }
}