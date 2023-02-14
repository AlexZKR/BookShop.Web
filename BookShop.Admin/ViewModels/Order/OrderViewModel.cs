using BookShop.Admin.Models.Order;

namespace BookShop.Admin.ViewModels.Order;

public class OrderViewModel : BaseViewModel
{
    public int OrderId {get; set;}
    public bool IsInProcess { get; set; }

    public int TotalItems { get; set; }
    public double TotalDiscount { get; set; }
    public double TotalPrice { get; set; }

    public string? BuyerId { get; set; }
    public string? BuyerName { get; set; }
    public string? PhoneNumber { get; set; }

    public string? PaymentType { get; set; }
    public string? DeliveryType { get; set; }
    public string? OrderComment { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();
}