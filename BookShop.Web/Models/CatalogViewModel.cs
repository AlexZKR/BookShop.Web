using BookShop.DAL.Entities;
using BookShop.DAL.Entities.Products;

namespace BookShop.Web.Models;

public class CatalogViewModel
{
    public List<Book> Books { get; set; } = null!;
    public List<Author> Authors { get; set; } = null!;

    public string? search { get; set; }

    //static data
    public List<string> Genres { get; set; } = null!;
    public List<string> Languages { get; set; } = null!;
    public List<string> Covers { get; set; } = null!;

}