using BookShop.BLL.Entities.BasketAggregate;

namespace BookShop.UnitTests.BLL.Entities.BasketTests;

public class BasketInfoProps
{
    private readonly int testCatalogItemId = 123;
    private readonly double testUnitPrice = 10;
    private readonly double testDiscount = 0.1;
    private readonly int testQuantity = 2;
    private readonly string testProductName = "test prodName";
    private readonly string buyerId = "Test buyerId";

    #region TotalItems

    [Fact]
    public void ReturnsTotalItemsWithNoItems()
    {
        Basket basket = new Basket(buyerId);

        var result = basket.TotalItems;

        Assert.Equal(0, result);
    }
    [Fact]
    public void ReturnsTotalItemsWithOneItem()
    {
        Basket basket = new Basket(buyerId);
        basket.AddItem(testCatalogItemId,testUnitPrice,testDiscount,testProductName,testQuantity);

        var result = basket.TotalItems;

        Assert.Equal(testQuantity, result);
    }

    [Fact]
    public void ReturnsTotalItemsWithMultipleItems()
    {
        var basket = new Basket(buyerId);
        basket.AddItem(testCatalogItemId,testUnitPrice,testDiscount,testProductName,testQuantity);
        basket.AddItem(testCatalogItemId,testUnitPrice,testDiscount,testProductName,testQuantity*2);

        var result = basket.TotalItems;

        Assert.Equal(testQuantity * 3, result);
    }
    #endregion

    #region TotalPrice

    [Fact]
    public void ReturnsTotalPriceWithNoItems()
    {
        Basket basket = new Basket(buyerId);

        var result = basket.TotalDiscountedPrice;

        Assert.Equal(0, result);
    }

    [Fact]
    public void ReturnsTotalPriceWithOneItem()
    {
        Basket basket = new Basket(buyerId);
        basket.AddItem(testCatalogItemId,testUnitPrice,testDiscount,testProductName,testQuantity);

        var result = basket.TotalDiscountedPrice;

        Assert.Equal((testUnitPrice - (testUnitPrice * testDiscount)) * testQuantity, result);
    }

    [Fact]
    public void ReturnsTotalPriceWithMultipleItems()
    {
        var basket = new Basket(buyerId);
        basket.AddItem(testCatalogItemId,testUnitPrice,testDiscount,testProductName,testQuantity);
        basket.AddItem(testCatalogItemId,testUnitPrice,testDiscount,testProductName,testQuantity*2);

        var result = basket.TotalDiscountedPrice;

        Assert.Equal(((testUnitPrice - (testUnitPrice * testDiscount)) * testQuantity) * 3, result);
    }

    #endregion

    #region TotalDiscountSize

    [Fact]
    public void ReturnsTotalDiscountSizeWithNoItems()
    {
        Basket basket = new Basket(buyerId);

        var result = basket.TotalDiscountSize;

        Assert.Equal(0, result);
    }

    [Fact]
    public void ReturnsTotalDiscountSizeWithOneItem()
    {
        Basket basket = new Basket(buyerId);
        basket.AddItem(testCatalogItemId,testUnitPrice,testDiscount,testProductName,testQuantity);

        var result = basket.TotalDiscountSize;

        Assert.Equal(2, result);
    }

    [Fact]
    public void ReturnsTotalDiscountSizeWithMultipleItems()
    {
        Basket basket = new Basket(buyerId);
        basket.AddItem(testCatalogItemId,testUnitPrice,testDiscount,testProductName,testQuantity);
        basket.AddItem(testCatalogItemId,testUnitPrice,testDiscount,testProductName,testQuantity*2);

        var result = basket.TotalDiscountSize;

        Assert.Equal(6, result);
    }

    #endregion

}