namespace BookShop.Web.Models.DTOs.Order;

public class OrderDTO
{
    public int OrderId {get; set;}
    public string? BuyerId { get; set; }
    public bool IsProccessed { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItemDTO>? OrderItems { get; set; }
}