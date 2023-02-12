namespace BookShop.Admin.ViewModels.Order;

public class OrderViewModel
{
    public int Id {get; set;}
    public string? BuyerId {get; set;}
    public DateTime OrderDate {get; set;}
    public bool IsProccessed {get; set;}
    public List<OrderItemViewModel> ItemViewModels { get; set; } = new List<OrderItemViewModel>();
}