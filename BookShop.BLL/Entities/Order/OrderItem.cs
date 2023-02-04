#pragma warning disable CS8618 // Required by Entity Framework

using System.ComponentModel.DataAnnotations;
using BookShop.BLL.Entities;

namespace BookShop.DAL.Entities.Order;
/// <summary>
/// Represents a snapshot of the item that was ordered. If catalog item details change, details of
/// the item that was part of a completed order should not change.
/// </summary>
public class OrderItem : BaseEntity
{
    // public CatalogItemOrdered ItemOrdered { get; private set; }
    //$$
    [Display(Name = "Полная стоимость товара, руб.")]
    [Range(0, int.MaxValue)]
    public double Price { get; set; } = 0;
    [Display(Name = "В наличии")]
    public int Units { get; set; } = 0;

    [Range(0, 1)]
    [Display(Name = "Скидка")]
    public double Discount { get; set; } = 0;
    [Display(Name = "Цена со скидкой")]
    public double DiscountedPrice => Price - (Price * Discount);

    // private OrderItem() { }

    // public OrderItem(CatalogItemOrdered itemOrdered, double unitPrice, int units)
    // {
    //     ItemOrdered = itemOrdered;
    //     UnitPrice = unitPrice;
    //     Units = units;
    // }

}