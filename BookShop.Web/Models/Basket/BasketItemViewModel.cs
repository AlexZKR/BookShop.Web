using System.ComponentModel.DataAnnotations;

namespace BookShop.Web.Models.Basket;

public class BasketItemViewModel
{
    public int Id { get; set; }
    public int BasketId { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public double FullPrice { get; set; }
    public double Discount { get; set; }
    public double DiscountedPrice { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be bigger than 0")]
    public int Quantity { get; set; }

    public string? PictureUrl { get; set; }
}
