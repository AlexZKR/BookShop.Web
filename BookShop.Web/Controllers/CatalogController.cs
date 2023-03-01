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
    private readonly IAppLogger<CatalogController> logger;

    public CatalogController(IFavouriteService<BaseProduct> favouriteService,
                             ICatalogViewModelService catalogViewModelService,
                             IRatingService ratingService,
                             IAppLogger<CatalogController> logger)
    {
        this.favouriteService = favouriteService;
        this.catalogViewModelService = catalogViewModelService;
        this.ratingService = ratingService;
        this.logger = logger;
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
    [HttpGet]
    public async Task<IActionResult> UpdateFav([FromQuery]int itemId)
    {
        await favouriteService.UpdateFavourite(HttpContext.GetUsername(), itemId.ToString());
        var check = favouriteService.CheckIfFavourite(HttpContext.GetUsername(), itemId);
        logger.LogInformation($"item id {itemId}; updated fav to: {check}");
        return Json(new
        {
            isFavourite = check
        });
    }

    [Authorize]
    public async Task<IActionResult> UpdateRating(int id, int rating, string returnUrl)
    {
        await ratingService.SetRating(HttpContext.GetUsername(),id,rating);
        return RedirectToAction(nameof(Index));
    }

}