using BookShop.DAL.Data;
using BookShop.DAL.Entities;
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
    public async Task<IActionResult> Index([FromQuery] string? search)
    {

        if (context == null) throw new NullReferenceException("Db is null");

        var vm = PopulateViewModelWithStaticData();

        if (!String.IsNullOrEmpty(search))
        {
            vm.Books = await FillBySearch(search);
        }
        else
        {
            vm.Books = await context.Books.ToListAsync();
        }
        vm.search = search;
        vm.Authors = await context.Authors.ToListAsync();

        return View(vm);
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