using Ardalis.GuardClauses;
using BookShop.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookShop.Web.Extensions;
using BookShop.BLL.Interfaces;
using BookShop.BLL.Entities.Products;

namespace BookShop.Web.Controllers;

public class BasketController : Controller
{
    private readonly IBasketViewModelService basketViewModelService;
    private readonly IRepository<BaseProduct> productRepository;
    private readonly BLL.Interfaces.IFavouriteService<BaseProduct> favouriteService;
    private readonly IBasketService basketService;

    public BasketController(IBasketViewModelService basketViewModelService,
    IRepository<BaseProduct> productRepository,
    IBasketService basketService,
    IFavouriteService<BaseProduct> favouriteService)
    {
        this.basketViewModelService = basketViewModelService;
        this.productRepository = productRepository;
        this.basketService = basketService;
        this.favouriteService = favouriteService;
        this.basketService = basketService;
    }
    public async Task<IActionResult> Index()
    {
        var vm = await basketViewModelService.GetOrCreateBasketForUser(ControllerBaseExtensions.GetOrSetBasketCookieAndUserName(this));
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


        var username = ControllerBaseExtensions.GetOrSetBasketCookieAndUserName(this);
        //if item was in favs, then remove it
        if(favouriteService.CheckIfFavourite(username,item) == true)
        {
            await favouriteService.RemoveFromFavourites(username,item);
        }

        var basket = await basketService.AddItemToBasket(username,
            id, item.FullPrice, item.Discount);

        return RedirectToAction(nameof(Index));
    }

    [Route("Remove/{id:int}")]
    public IActionResult Remove(int id)
    {
        var username = ControllerBaseExtensions.GetOrSetBasketCookieAndUserName(this);
        basketService.RemoveItemFromBasket(username, id);
        return RedirectToAction(nameof(Index));
    }

    [Route("ChangeQuantity/{itemId:int}/{mode}")]
    public IActionResult ChangeQuantity(int itemId, string mode)
    {
        string username = ControllerBaseExtensions.GetOrSetBasketCookieAndUserName(this);
        basketService.UpDownQuantity(username, itemId, mode);
        return RedirectToAction(nameof(Index));
        // var vm = await basketViewModelService.GetOrCreateBasketForUser(ControllerBaseExtensions.GetOrSetBasketCookieAndUserName(this));

        // return PartialView("_BasketListPartial", vm);
    }

}