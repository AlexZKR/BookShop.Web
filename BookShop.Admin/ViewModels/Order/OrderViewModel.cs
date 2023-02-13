using BookShop.Admin.Models.Order;

namespace BookShop.Admin.ViewModels.Order;

public class OrderViewModel : BaseViewModel
{
    public int OrderId {get; set;}
    public string? BuyerId { get; set; }
    public bool IsProccessed { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItemDTO>? OrderItems { get; set; }
}