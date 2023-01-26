namespace BookShop.DAL.Entities.Basket;

public class Basket : BaseEntity
{
    public string BuyerId { get; private set; }
    private readonly List<BasketItem> items = new List<BasketItem>();
    public IReadOnlyCollection<BasketItem> Items => items.AsReadOnly();
    public int TotalItems => items.Sum(i => i.Quantity);

    public Basket(string buyerId)
    {
        BuyerId = buyerId;
    }

    public void AddItem(int itemId, double unitPrice, int quantity = 1)
    {
        if (!Items.Any(i => i.ProductId == itemId))
        {
            items.Add(new BasketItem(itemId, quantity, unitPrice));
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