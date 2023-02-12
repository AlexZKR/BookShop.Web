namespace BookShop.Admin.ViewModels.Order;


public class OrderItemViewModel
{
    public int Id {get; set;}
    public string? Name { get; set; }
    public double FullPrice { get; set; }
    public double Discount { get; set; }
    public double DiscountedPrice => FullPrice - (FullPrice * Discount);
    public int Units { get; set; }
}