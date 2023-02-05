using BookShop.BLL.Entities.Products;
using BookShop.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookShop.BLL;
using BookShop.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;

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
        string username = GetUserName();
        var catalogModel = await catalogViewModelService
        .GetCatalogItems(username, pageId ?? 0, SD.ITEMS_PER_PAGE, searchQuery: searchQuery, AuthorId: author, genre: genre, lang: lang!, cover: cover);

        return View(catalogModel);
    }

    [Authorize]
    [Route("UpdateFav/{prodId:int}/{returnUrl}")]
    public async Task<IActionResult> UpdateFav(int prodId, string returnUrl)
    {
        string username = GetUserName();
        await favouriteService.UpdateFavourite(username, prodId.ToString());
        // if (returnUrl.Contains("Product"))
        //     return Redirect($"http://localhost:5092/{returnUrl}");
        return RedirectToAction(nameof(Index));
    }





    //private helpers
    //todo refactor repeating code in all controllers
    private string GetUserName()
    {
        var user = Request.HttpContext.User;
        if (user.Identity == null) throw new NullReferenceException();
        string? userName = null;

        if (user.Identity.IsAuthenticated)
        {
            if (user.Identity.Name != null)
                return user.Identity.Name!;
        }

        return userName!;
    }


    //n layered controller

    // [Authorize]
    // [Route("UpdateFav/{prodId:int}")]
    // public async Task<ActionResult> UpdateFav(int prodId)
    // {
    //     var user = await userManager.GetUserAsync(User);

    //     if (user == null) return Redirect("/Identity/Account/Errors/AccessDenied");

    //     var exists = context.Books.FirstOrDefault(b => b.Id == prodId)!.IsFavourite = await favouriteService
    //     .UpdateFavourite(user, prodId.ToString());

    //     return RedirectToAction("Index");
    // }

    // private async Task<List<Book>> FillBySearch(string search)
    // {
    //     var books = await context.Books.Include(b => b.Author).ToListAsync();
    //     books = books.Where(
    //         b =>
    //     b.Name.ToLower().Contains(search.ToLower()) ||
    //     b.Author.Name!.ToLower()!.Contains(search.ToLower())
    //     ).ToList()!;


    //     return books;
    // }

    /// <summary>
    /// Creates and populates vm with static display values from enums
    /// </summary>
    /// <returns></returns>
    // private async Task<CatalogViewModel> PopulateViewModelWithStaticData()
    // {
    //     var genres = EnumHelper<Genre>.GetDisplayValues(Genre.Fiction).ToList();
    //     var languages = EnumHelper<Language>.GetDisplayValues(Language.Russian).ToList();
    //     var covers = EnumHelper<Cover>.GetDisplayValues(Cover.HardCover).ToList();

    //     var vm = new CatalogViewModel
    //     {
    //         Genres = genres,
    //         Languages = languages,
    //         Covers = covers
    //     };
    //     vm.Authors = await context.Authors.ToListAsync();
    //     return vm;
    // }

}