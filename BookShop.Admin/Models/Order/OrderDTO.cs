using Newtonsoft.Json;

namespace BookShop.Admin.Models.Order;

public class OrderDTO
{
    [JsonProperty("orderId")]
    public int OrderId {get; set;}
    [JsonProperty("buyerId")]
    public string? BuyerId { get; set; }
    [JsonProperty("isInProcess")]
    public bool IsProccessed { get; set; }
    [JsonProperty("orderDate")]
    public DateTime OrderDate { get; set; }
    [JsonProperty("orderItems")]
    public List<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
}