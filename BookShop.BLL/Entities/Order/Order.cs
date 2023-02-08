#pragma warning disable CS8618 // Required by Entity Framework


using BookShop.BLL.Interfaces;

namespace BookShop.BLL.Entities.Order;

public class Order : BaseEntity, IAggregateRoot
{
    public bool IsInProcess {get; set;} = true;

    public int TotalItems => OrderItems.Sum(i => i.Units);
    public double TotalDiscount => OrderItems.Sum(i => (i.FullPrice - i.DiscountedPrice));
    public double TotalPrice => OrderItems.Sum(i => i.DiscountedPrice);
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public OrderInfo OrderInfo { get; set; }
    public Address Address { get; set; }
    public Buyer Buyer { get; set; }


    public Order(Address Address, Buyer Buyer, OrderInfo OrderInfo)
    {
        this.Address = Address;
        this.Buyer = Buyer;
        this.OrderInfo = OrderInfo;
    }

    public Order() { }
}