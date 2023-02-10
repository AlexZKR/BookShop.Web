using BookShop.BLL.Interfaces;

namespace BookShop.BLL.Entities.Basket;

public class BasketItem : BaseEntity, ICatalogAggregateRoot
{
    public double FullPrice { get; private set; }
    public double Discount { get; set; }
    public double DiscountedPrice => FullPrice - (FullPrice * Discount);

    public int Quantity { get; private set; }
    public int ProductId { get; private set; }
    public int BasketId { get; private set; }

    public BasketItem(int productId, int quantity, double fullPrice, double discount)
    {
        ProductId = productId;
        FullPrice = fullPrice;
        Discount = discount;
        SetQuantity(quantity);
    }

    public void AddQuantity(int quantity)
    {
        if (quantity < int.MaxValue)
            Quantity += quantity;
    }
    public void DecreaseQuantity(int quantity)
    {
        if (quantity > 0)
            Quantity -= quantity;

    }

    public void SetQuantity(int quantity)
    {
        if (quantity < int.MaxValue)
            Quantity = quantity;
    }
}