using BookShop.BLL.Entities.BasketAggregate;

namespace BookShop.UnitTests.BLL.Entities.BasketTests;

public class BasketRemoveEmptyItems
{
    private readonly int testCatalogItemId = 123;
    private readonly double testUnitPrice = 10;
    private readonly double testDiscount = 0.1;
    private readonly string testProductName = "test prodName";
    private readonly string buyerId = "Test buyerId";

    [Fact]
    public void RemoveEmptyItems()
    {
        Basket basket = new Basket(buyerId);

        basket.AddItem(testCatalogItemId,testUnitPrice,testDiscount,testProductName,0);

        Assert.Equal(0, basket.TotalItems);
        basket.RemoveEmptyItems();
        Assert.Equal(0, basket.Items.Count);
    }
}