using BookShop.BLL.Entities;
using BookShop.BLL.Entities.Enums;
using BookShop.BLL.Entities.Order;
using BookShop.BLL.Interfaces;

namespace BookShop.DAL.Entities.Order;

public class Order : BaseEntity, IAggregateRoot
{
    public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.Now;
    public Address? ShipToAddress { get; private set; }
    public Buyer? Buyer { get; set; }

    public PaymentType PaymentType { get; set; }
    public DeliveryType DeliveryType { get; set; }

    private readonly List<OrderItem> _orderItems = new List<OrderItem>();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public string? OrderComment { get; set; }

    // public Order(string buyerId, Address shipTo, List<OrderItem> items)
    // {
    //     Guard.Against.Null(buyerId, nameof(buyerId));
    //     BuyerId = buyerId;
    //     ShipToAddress = shipTo;
    //     _orderItems = items;
    // }

    public double Total()
    {
        double total = 0;
        foreach (var item in _orderItems)
        {
            if (item.DiscountedPrice != 0)
                total += item.DiscountedPrice * item.Units;
            else
                total += item.Price * item.Units;
        }
        return total;
    }

}