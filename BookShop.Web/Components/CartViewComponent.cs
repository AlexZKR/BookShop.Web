using BookShop.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.WebComponents;

public class CartViewComponent : ViewComponent
{
    private readonly IBasketQueryService basketQueryService;

    public CartViewComponent(IBasketQueryService basketQueryService)
    {
        this.basketQueryService = basketQueryService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string username)
    {
        if (username == null) username = new Guid().ToString();

        ViewData["basketCount"] = await basketQueryService.CountTotalBasketItemsAsync(username);
        return View();
    }

}