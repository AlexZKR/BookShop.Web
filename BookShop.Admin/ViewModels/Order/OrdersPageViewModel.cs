using System.ComponentModel.DataAnnotations;

namespace BookShop.Admin.ViewModels.Order;

public class OrdersPageViewModel : BaseViewModel
{
    //public BuyerViewModel? Buyer { get; set; }
    public string? BuyerName { get; set; }
    public int UnproccessedOrdersCount { get; set; }

    public List<BuyerViewModel> Buyers { get; set; } = new List<BuyerViewModel>();
    //Contains orders not related to specific user
    [Display(Name = "Последние заказы")]
    public List<OrderViewModel> UnproccessedOrders { get; set; } = new List<OrderViewModel>();
    [Display(Name = "История заказов")]
    public List<OrderViewModel> ProccessedOrders { get; set; } = new List<OrderViewModel>();
}