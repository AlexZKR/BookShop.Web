
namespace BookShop.Web.Models.Catalog;

public class CatalogItemViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? AuthorName { get; set; }
    public string? PictureUri { get; set; }

    public double Price { get; set; }
    public double DiscountedPrice { get; set; }

    public bool IsOnDiscount { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsFavourite { get; set; }

    public RatingViewModel Rating {get; set; } = new RatingViewModel();

}