#pragma warning disable CS8618 // Required by Entity Framework

using BookShop.BLL.Entities.Enums;

namespace BookShop.BLL.Entities.Order;

public class OrderInfo //Value object
{
    public OrderInfo()
    {
    }

    public OrderInfo(DateTime OrderDate, PaymentType PaymentType, DeliveryType DeliveryType, string? OrderComment)
    {
        this.OrderDate = OrderDate;
        this.PaymentType = PaymentType;
        this.DeliveryType = DeliveryType;
        this.OrderComment = OrderComment;
    }

    public DateTime OrderDate { get; private set; } = DateTime.Now;
    public PaymentType PaymentType { get; set; }
    public DeliveryType DeliveryType { get; set; }
    public string? OrderComment { get; set; }
}