using BookShop.BLL.Entities;
using BookShop.BLL.Interfaces;

namespace BookShop.DAL.Entities.Order;

public class Order : BaseEntity, IAggregateRoot
{
    public string BuyerId { get; set; }

    private readonly List<OrderItem> _orderItems = new List<OrderItem>();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public Order(string buyerId)
    {
        BuyerId = buyerId;
    }

}