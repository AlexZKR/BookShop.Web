namespace BookShop.Web.Models.Order;
public class OrderViewModel
{
    public bool IsInProcess {get; set;} = true;

    public int OrderId { get; set; }
    public string? BuyerId { get; set; }
    public double TotalPrice { get; set; }
    public double FullPrice { get; set; }
    public double TotalDiscount { get; set; }
    public string? BuyerFirstName { get; set; }
    public string? BuyerLastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Region { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostCode { get; set; }
    public string? OrderDate { get; set; }
    public string? OrderComment { get; set; }
    public string? DeliveryType { get; set; }
    public string? PaymentType { get; set; }

    public List<OrderItemViewModel> Items { get; set; } = new List<OrderItemViewModel>();

    //public int TotalItems => Items.Count
    public int Units {get; set;}
}