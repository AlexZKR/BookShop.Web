namespace BookShop.Admin.Models.Order;

public class OrderDTO
{
    public int OrderId {get; set;}
    public string? BuyerId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItemDTO>? OrderItems { get; set; }
}