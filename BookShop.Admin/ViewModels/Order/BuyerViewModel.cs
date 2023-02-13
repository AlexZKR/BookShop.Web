namespace BookShop.Admin.ViewModels.Order;

public class BuyerViewModel : BaseViewModel
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public int Discount { get; set; }
    public string? PhoneNumber { get; set; }
    public int UnproccessedOrdersCount { get; set; }
    public List<OrderViewModel> ProccessedOrders { get; set; } = new List<OrderViewModel>();
    public List<OrderViewModel> UnproccessedOrders { get; set; } = new List<OrderViewModel>();
}