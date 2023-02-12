namespace BookShop.Admin.ViewModels.Order;

public class OrdersPageViewModel
{
    public string? StatusMessage { get; set; }
    public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    //Contains orders not related to specific user
    public List<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
}