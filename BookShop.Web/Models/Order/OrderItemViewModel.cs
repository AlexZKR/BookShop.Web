namespace BookShop.Web.Models.Order;
public class OrderItemViewModel
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public double FullPrice { get; set; }
    public double DiscountedPrice { get; set; }
    public double Discount { get; set; }
    public int Units { get; set; }
    public string? PictureUrl { get; set; }
    public string? AddInfo { get; set; }
}