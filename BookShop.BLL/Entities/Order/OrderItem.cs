#pragma warning disable CS8618 // Required by Entity Framework

using System.ComponentModel.DataAnnotations.Schema;
using BookShop.BLL.Interfaces;

namespace BookShop.BLL.Entities.Order;
/// <summary>
/// Represents a snapshot of the item that was ordered. If catalog item details change, details of
/// the item that was part of a completed order should not change.
/// </summary>
public class OrderItem : BaseEntity, IAggregateRoot
{
    public OrderItem(
        int ProductId,
        string? ProductName,
        double FullPrice,
        double Discount,
        int Units)
    {

        this.ProductId = ProductId;
        this.ProductName = ProductName;
        this.FullPrice = FullPrice;
        this.Discount = Discount;
        this.Units = Units;
    }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public double FullPrice { get; set; }
    public double TotalPrice => FullPrice * Units;
    public double DiscountedPrice => TotalPrice - ((FullPrice - (FullPrice * Discount)) * Units);
    public double Discount { get; set; }
    public int Units { get; set; }
    public string? AddInfo { get; set; }

}