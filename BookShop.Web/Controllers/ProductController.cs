using BookShop.BLL.Entities.Products;
using BookShop.BLL.Interfaces;
using BookShop.Web.Extensions;
using BookShop.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductDetailsViewModelService productDetailsViewModelService;
    private readonly IFavouriteService<BaseProduct> favouriteService;

    public ProductController(IProductDetailsViewModelService productDetailsViewModelService,
    IFavouriteService<BaseProduct> favouriteService)
    {
        this.productDetailsViewModelService = productDetailsViewModelService;
        this.favouriteService = favouriteService;
    }
    public async Task<IActionResult> Index(int id)
    {
        string username = HttpContext.GetUsername();
        var vm = await productDetailsViewModelService.CreateViewModel(id, username);
        return View(vm);
    }


    //exact same method on both controllers refactor
    [Authorize]
    [Route("UpdFav/{prodId:int}/{returnUrl}")]
    public async Task<IActionResult> UpdFav(int prodId, string returnUrl)
    {
        string username = HttpContext.GetUsername();
        await favouriteService.UpdateFavourite(username, prodId.ToString());
        return RedirectToAction(nameof(Index));
    }

    // //todo refactor repeating code in all controllers
    // private string GetUserName()
    // {
    //     var user = Request.HttpContext.User;
    //     if (user.Identity == null) throw new NullReferenceException();
    //     string? userName = null;

    //     if (user.Identity.IsAuthenticated)
    //     {
    //         if (user.Identity.Name != null)
    //             return user.Identity.Name!;
    //     }

    //     return userName!;
    // }

}