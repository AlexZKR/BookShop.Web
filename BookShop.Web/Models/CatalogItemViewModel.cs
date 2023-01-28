
namespace BookShop.Web.Models;

public class CatalogItemViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? AuthorName { get; set; }
    public string? PictureUri { get; set; }

    public double Price { get; set; }
    public double DiscountedPrice { get; set; }

}