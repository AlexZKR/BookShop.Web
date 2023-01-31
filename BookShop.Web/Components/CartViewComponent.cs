using BookShop.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.WebComponents;

public class CartViewComponent : ViewComponent
{
    private readonly IBasketViewModelService basketViewModelService;

    public CartViewComponent(IBasketViewModelService basketViewModelService)
    {
        this.basketViewModelService = basketViewModelService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string username)
    {
        if (username == null) username = new Guid().ToString();
        var vm = await basketViewModelService.GetOrCreateBasketForUser(username);
        return View(vm);
    }
}