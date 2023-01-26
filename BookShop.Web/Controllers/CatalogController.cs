using BookShop.DAL.Data;
using BookShop.DAL.Entities;
using BookShop.DAL.Entities.Products;
using BookShop.Web.Models;
using BookShop.Web.Services.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Web.Controllers;

public class CatalogController : Controller
{
    private readonly AppDbContext context;
    private readonly appIdentityDbContext identityDbContext;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IFavouriteService<Book> favouriteService;

    public CatalogController(AppDbContext context,
     appIdentityDbContext identityDbContext,
     UserManager<ApplicationUser> userManager,
     IFavouriteService<Book> favouriteService)
    {
        this.context = context;
        this.identityDbContext = identityDbContext;
        this.userManager = userManager;
        this.favouriteService = favouriteService;
    }
    public async Task<IActionResult> Index([FromQuery] string? search)
    {

        if (context == null) throw new NullReferenceException("Db is null");

        var vm = await PopulateViewModelWithStaticData();

        if (!String.IsNullOrEmpty(search))
        {
            vm.Books = await FillBySearch(search);
        }
        else
        {
            //fill by pages
            vm.Books = await context.Books.ToListAsync();
        }
        var user = await userManager.GetUserAsync(User);

        if (user != null)
            vm.Books = favouriteService.CheckFavourites(user, vm.Books);

        vm.search = search;

        return View(vm);
    }


    [Authorize]
    [Route("UpdateFav/{prodId:int}")]
    public async Task<ActionResult> UpdateFav(int prodId)
    {
        var user = await userManager.GetUserAsync(User);

        if (user == null) return Redirect("/Identity/Account/Errors/AccessDenied");

        var exists = context.Books.FirstOrDefault(b => b.Id == prodId)!.IsFavourite = await favouriteService
        .UpdateFavourite(user, prodId.ToString());

        return RedirectToAction("Index");
    }

    private async Task<List<Book>> FillBySearch(string search)
    {
        var books = await context.Books.Include(b => b.Author).ToListAsync();
        books = books.Where(
            b =>
        b.Name.ToLower().Contains(search.ToLower()) ||
        b.Author.Name!.ToLower()!.Contains(search.ToLower())
        ).ToList()!;


        return books;
    }

    /// <summary>
    /// Creates and populates vm with static display values from enums
    /// </summary>
    /// <returns></returns>
    private async Task<CatalogViewModel> PopulateViewModelWithStaticData()
    {
        var genres = EnumHelper<Genre>.GetDisplayValues(Genre.Fiction).ToList();
        var languages = EnumHelper<Language>.GetDisplayValues(Language.Russian).ToList();
        var covers = EnumHelper<Cover>.GetDisplayValues(Cover.HardCover).ToList();

        var vm = new CatalogViewModel
        {
            Genres = genres,
            Languages = languages,
            Covers = covers
        };
        vm.Authors = await context.Authors.ToListAsync();
        return vm;
    }

}