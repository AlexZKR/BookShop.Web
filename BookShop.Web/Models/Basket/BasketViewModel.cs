namespace BookShop.Web.Models.Basket;

public class BasketViewModel
{
    public int Id { get; set; }
    public List<BasketItemViewModel> Items { get; set; } = new List<BasketItemViewModel>();
    public string? BuyerId { get; set; }

    public int TotalItems { get; set; }
    public double TotalDiscount { get; set; }
    public double TotalPrice { get; set; }

    // public double Total()
    // {
    //     return Math.Round(Items.Sum(x => x.DiscountedPrice * x.Quantity), 2);
    // }
}
