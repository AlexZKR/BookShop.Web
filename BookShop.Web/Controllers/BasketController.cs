using Ardalis.GuardClauses;
using BookShop.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookShop.BLL;
using BookShop.BLL.Interfaces;
using BookShop.BLL.Entities.Products;
using BookShop.Web.Models.Basket;

namespace BookShop.Web.Controllers;

public class BasketController : Controller
{
    private readonly IBasketViewModelService basketViewModelService;
    private readonly IRepository<Book> productRepository;
    private readonly IBasketService basketService;

    public BasketController(IBasketViewModelService basketViewModelService,
    IRepository<Book> productRepository,
    IBasketService basketService)
    {
        this.basketViewModelService = basketViewModelService;
        this.productRepository = productRepository;
        this.basketService = basketService;
    }
    public async Task<IActionResult> Index()
    {
        var vm = await basketViewModelService.GetOrCreateBasketForUser(GetOrSetBasketCookieAndUserName());
        return View(vm);
    }
    [Route("AddToCart/{id:int}")]
    public async Task<IActionResult> AddToCart(int id)
    {
        var item = await productRepository.GetByIdAsync(id);
        if (item == null)
        {
            return RedirectToPage("/Index");
        }

        var username = GetOrSetBasketCookieAndUserName();
        var basket = await basketService.AddItemToBasket(username,
            id, item.Price);

        // var vm = await basketViewModelService.Map(basket);
        return RedirectToAction(nameof(Index));
    }

    [Route("Remove/{id:int}")]
    public IActionResult Remove(int id)
    {
        var username = GetOrSetBasketCookieAndUserName();
        basketService.RemoveItemFromBasket(username, id);
        return RedirectToAction(nameof(Index));
    }

    [Route("ChangeQuantity/{itemId:int}/{mode}")]
    public IActionResult ChangeQuantity(int itemId, string mode)
    {
        string username = GetOrSetBasketCookieAndUserName();
        basketService.UpDownQuantity(username, itemId, mode);
        return RedirectToAction(nameof(Index));
    }

    //private helpers

    //Even unauth users can create their cart. If they want to proceed with it, they will have to register
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