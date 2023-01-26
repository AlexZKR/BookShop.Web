namespace BookShop.DAL.Entities.Basket;

public class BasketItem : BaseEntity
{
    public double UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public int ProductId { get; private set; }
    public int BasketId { get; private set; }

    public BasketItem(int itemId, int quantity, double unitPrice)
    {
        ProductId = itemId;
        UnitPrice = unitPrice;
        SetQuantity(quantity);
    }

    public void AddQuantity(int quantity)
    {
        if (quantity < int.MaxValue)
            Quantity += quantity;
    }

    public void SetQuantity(int quantity)
    {
        if (quantity < int.MaxValue)
            Quantity = quantity;
    }
}