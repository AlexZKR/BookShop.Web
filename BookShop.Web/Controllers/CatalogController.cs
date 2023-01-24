using BookShop.DAL.Data;
using BookShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Web.Controllers;

public class CatalogController : Controller
{
    private readonly AppDbContext context;

    public CatalogController(AppDbContext context)
    {
        this.context = context;
    }
    public async Task<IActionResult> Index()
    {

        if (context == null) throw new NullReferenceException("Db is null");

        var vm = PopulateViewModelWithStaticData();

        var books = await context.Books.ToListAsync();
        var authors = await context.Authors.ToListAsync();


        vm.Books = books;
        vm.Authors = authors;

        return View(vm);
    }

    /// <summary>
    /// Creates and populates vm with static display values from enums
    /// </summary>
    /// <returns></returns>
    private CatalogViewModel PopulateViewModelWithStaticData()
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
        return vm;
    }
}