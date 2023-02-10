using BookShop.BLL.Entities.Products;
using BookShop.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookShop.BLL;
using BookShop.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using BookShop.Web.Extensions;

namespace BookShop.Web.Controllers;

public class CatalogController : Controller
{
    private readonly IFavouriteService<BaseProduct> favouriteService;
    private readonly ICatalogViewModelService catalogViewModelService;

    public CatalogController(IFavouriteService<BaseProduct> favouriteService,
    ICatalogViewModelService catalogViewModelService)
    {
        this.favouriteService = favouriteService;
        this.catalogViewModelService = catalogViewModelService;
    }
    public async Task<IActionResult> Index([FromQuery] string? searchQuery, int? pageId, int? author, int? cover, int? genre, int? lang)
    {
        //todo: even not auth users can get userfavs object. thats not right
        string username = HttpContext.GetUsername();
        var catalogModel = await catalogViewModelService
        .GetCatalogItems(username, pageId ?? 0, SD.ITEMS_PER_PAGE, searchQuery: searchQuery, AuthorId: author, genre: genre, lang: lang!, cover: cover);

        return View(catalogModel);
    }

    [Authorize]
    [Route("UpdateFav/{prodId:int}/{returnUrl}")]
    [Route("UpdateFav/{returnUrl}")]
    [Route("UpdateFav")]
    public async Task<IActionResult> UpdateFav(int prodId, string returnUrl)
    {
        string username = HttpContext.GetUsername();
        await favouriteService.UpdateFavourite(username, prodId.ToString());
        // if (returnUrl.Contains("Product"))
        //     return Redirect($"http://localhost:5092/{returnUrl}");
        return RedirectToAction(nameof(Index));
    }

}