using System.ComponentModel.DataAnnotations;

namespace BookShop.Web.Models;

public class BasketItemViewModel
{
    public int Id { get; set; }
    public int CatalogItemId { get; set; }
    public string? ProductName { get; set; }
    public double UnitPrice { get; set; }
    public double OldUnitPrice { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be bigger than 0")]
    public int Quantity { get; set; }

    public string? PictureUrl { get; set; }
}
