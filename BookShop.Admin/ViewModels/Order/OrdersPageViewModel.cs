namespace BookShop.Admin.ViewModels.Order;

public class OrdersPageViewModel
{
    public string? StatusMessage { get; set; }
    public UserViewModel? User { get; set; }
    public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    //Contains orders not related to specific user
    public List<OrderViewModel> UnproccessedOrders { get; set; } = new List<OrderViewModel>();
    public List<OrderViewModel> ProccessedOrders { get; set; } = new List<OrderViewModel>();
}