using BookShop.BLL.Entities.Products;
using BookShop.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookShop.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using BookShop.Web.Extensions;

namespace BookShop.Web.Controllers;

public class CatalogController : Controller
{
    private readonly IFavouriteService<BaseProduct> favouriteService;
    private readonly ICatalogViewModelService catalogViewModelService;
    private readonly IRatingService ratingService;

    public CatalogController(IFavouriteService<BaseProduct> favouriteService,
                             ICatalogViewModelService catalogViewModelService,
                             IRatingService ratingService)
    {
        this.favouriteService = favouriteService;
        this.catalogViewModelService = catalogViewModelService;
        this.ratingService = ratingService;
    }
    public async Task<IActionResult> Index([FromQuery] string? SearchQuery,
                                           int? pageId,
                                           int author,
                                           int? cover,
                                           int? genre,
                                           int? lang)
    {
        string username = HttpContext.GetUsername();
        var catalogModel = await catalogViewModelService
        .GetCatalogViewModel(username,SearchQuery, pageId ?? 0,  AuthorId: author, genre: genre, lang: lang!, cover: cover);

        return View(catalogModel);
    }

    [Authorize]
    [Route("UpdateFav/{prodId:int}/{returnUrl}")]
    [Route("UpdateFav/{returnUrl}")]
    [Route("UpdateFav")]
    public async Task<IActionResult> UpdateFav(int prodId, string returnUrl)
    {
        await favouriteService.UpdateFavourite(HttpContext.GetUsername(), prodId.ToString());
        // if (returnUrl.Contains("Product"))
        //     return Redirect($"http://localhost:5092/{returnUrl}");
        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    public async Task<IActionResult> UpdateRating(int id, int rating, string returnUrl)
    {
        await ratingService.SetRating(HttpContext.GetUsername(),id,rating);
        return RedirectToAction(nameof(Index));
    }

}