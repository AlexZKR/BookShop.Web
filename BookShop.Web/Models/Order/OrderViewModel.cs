namespace BookShop.Web.Models.Order;
public class OrderViewModel
{
    public int OrderId { get; set; }
    public string? BuyerId { get; set; }
    public double TotalPrice { get; set; }
    public double TotalDiscount { get; set; }
    public string? BuyerFirstName { get; set; }
    public string? BuyerLastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ShipmentAddress { get; set; }
    public string? OrderDate { get; set; }

    public List<OrderItemViewModel> Items { get; set; } = new List<OrderItemViewModel>();
}