using BookShop.BLL.Interfaces;

namespace BookShop.BLL.Entities.Basket;

public class Basket : BaseEntity, IBasketAggregateRoot
{
    public string BuyerId { get; private set; }
    private readonly List<BasketItem> items = new List<BasketItem>();
    public IReadOnlyCollection<BasketItem> Items => items.AsReadOnly();
    public int TotalItems => items.Sum(i => i.Quantity);
    public double TotalDiscount => items.Sum(i => (i.FullPrice - i.DiscountedPrice));
    public double TotalPrice => items.Sum(i => i.DiscountedPrice);

    public Basket(string buyerId)
    {
        BuyerId = buyerId;
    }

    public void AddItem(int itemId, double fullPrice, double discount, int quantity = 1)
    {
        if (!Items.Any(i => i.ProductId == itemId))
        {
            items.Add(new BasketItem(itemId, quantity, fullPrice, discount));
            return;
        }
        var existingItem = Items.First(i => i.ProductId == itemId);
        existingItem.AddQuantity(quantity);
    }

    public void RemoveEmptyItems()
    {
        items.RemoveAll(i => i.Quantity == 0);
    }

}