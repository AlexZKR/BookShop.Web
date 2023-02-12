namespace BookShop.Admin.ViewModels.Order;

public class OrdersPageViewModel
{
    public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    //Contains orders not related to specific user
    public List<OrderItemViewModel> Orders { get; set; } = new List<OrderItemViewModel>();
}