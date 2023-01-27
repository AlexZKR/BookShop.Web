using BookShop.BLL.Entities;
using BookShop.BLL.Entities.Products;

namespace BookShop.Web.Models;

public class ProductViewModel
{
    public Book Book { get; set; } = null!;
    public List<Book> Related { get; set; } = null!;

    //static data
    public string Genre { get; set; } = null!;
    public string Language { get; set; } = null!;
    public string Cover { get; set; } = null!;

}