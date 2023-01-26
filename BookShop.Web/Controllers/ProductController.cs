using BookShop.DAL.Data;
using BookShop.DAL.Entities.Products;
using BookShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Web.Controllers;

public class ProductController : Controller
{
    private readonly AppDbContext context;

    public ProductController(AppDbContext context)
    {
        this.context = context;
    }
    [Route("Product")]
    public async Task<IActionResult> Index(int id)
    {
        if (context == null) throw new NullReferenceException("Db null");

        var book = await context.Books
        .Include(b => b.Author)
        .FirstOrDefaultAsync(b => b.Id == id)!;

        var vm = await PopulateViewModelWithStaticData(book!);

        vm.Related = await context.Books
        .Include(b => b.Author)
        .Where(b => b.Genre == book!.Genre)
        .Take(9)
        .ToListAsync();

        return View("ProductPage", vm);
    }

    private async Task<ProductViewModel> PopulateViewModelWithStaticData(Book book)
    {
        var genre = EnumHelper<Genre>.GetDisplayValue(Genre.Fiction);
        var language = EnumHelper<Language>.GetDisplayValue(Language.Russian);
        var cover = EnumHelper<Cover>.GetDisplayValue(Cover.HardCover);

        book.Author = (await context.Authors.FirstOrDefaultAsync(a => a.Books.Contains(book)))!;

        var vm = new ProductViewModel
        {
            Book = book,
            Genre = genre,
            Language = language,
            Cover = cover
        };
        return vm;
    }
}