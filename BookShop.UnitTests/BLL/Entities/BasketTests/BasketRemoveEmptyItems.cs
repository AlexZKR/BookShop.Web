using BookShop.BLL.Entities.BasketAggregate;

namespace BookShop.BLL.Entities.BasketTests;

public class BasketRemoveEmptyItems
{
    private readonly int testCatalogItemId = 123;
    private readonly double testUnitPrice = 10;
    private readonly double testDiscount = 0.1;
    private readonly int testQuantity = 2;
    private readonly string buyerId = "Test buyerId";

    [Fact]
    public void RemoveEmptyItems()
    {
        Basket basket = new Basket(buyerId);

        basket.AddItem(testCatalogItemId,testUnitPrice,testDiscount,0);

        Assert.Equal(0, basket.TotalItems);
        basket.RemoveEmptyItems();
        Assert.Equal(0, basket.Items.Count);
    }
}