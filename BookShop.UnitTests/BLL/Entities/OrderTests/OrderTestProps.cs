using BookShop.BLL.Entities.Order;

namespace BookShop.UnitTests.BLL.Entities.OrderTests;

public class OrderTestProps
{
    public Address? testAddress = null;
    public Buyer? testBuyer = null;
    public OrderInfo? testOrderInfo = null;

    private readonly int testCatalogItemId = 123;
    private readonly string testProdName = "testName";
    private readonly double testUnitPrice = 10;
    private readonly double testDiscount = 0.1;
    private readonly int testQuantity = 2;


    #region TotalItems

    [Fact]
    public void ReturnsTotalItemsWithNoItems()
    {
        Order order = new Order(testAddress!, testBuyer!, testOrderInfo!);

        var result = order.TotalItems;
        Assert.Equal(0, result);
    }

    [Fact]
    public void ReturnsTotalItemsWithOneItem()
    {
        Order order = new Order(testAddress!, testBuyer!, testOrderInfo!);

        OrderItem item = new OrderItem(testCatalogItemId, testProdName, testUnitPrice, testDiscount, testQuantity);
        order.OrderItems.Add(item);

        var result = order.TotalItems;
        Assert.Equal(testQuantity, result);
    }
    [Fact]
    public void ReturnsTotalItemsWithMultipleItems()
    {
        Order order = new Order(testAddress!, testBuyer!, testOrderInfo!);

        OrderItem item = new OrderItem(testCatalogItemId, testProdName, testUnitPrice, testDiscount, testQuantity);
        OrderItem item1 = new OrderItem(testCatalogItemId + 1, testProdName, testUnitPrice, testDiscount, testQuantity);
        OrderItem item2 = new OrderItem(testCatalogItemId + 1, testProdName, testUnitPrice, testDiscount, testQuantity);
        order.OrderItems.Add(item);
        order.OrderItems.Add(item1);
        order.OrderItems.Add(item2);

        var result = order.TotalItems;
        Assert.Equal(testQuantity * 3, result);
    }

    #endregion

    #region TotalDiscount

    [Fact]
    public void ReturnsTotalDiscountWithNoItems()
    {
        Order order = new Order(testAddress!, testBuyer!, testOrderInfo!);

        var result = order.TotalDiscount;
        Assert.Equal(0, result);
    }

    [Fact]
    public void ReturnsTotalDiscountWithOneItem()
    {
        Order order = new Order(testAddress!, testBuyer!, testOrderInfo!);

        OrderItem item = new OrderItem(testCatalogItemId, testProdName, testUnitPrice, testDiscount, testQuantity);
        order.OrderItems.Add(item);

        var totalTestQuantity = testQuantity * order.OrderItems.Count;
        var result = order.TotalDiscount;
        Assert.Equal(((testUnitPrice*testQuantity)-(testUnitPrice*testQuantity-(testUnitPrice*testDiscount)))*totalTestQuantity, result);
    }

    [Fact]
    public void ReturnsTotalDiscountWithMultipleItems()
    {
        Order order = new Order(testAddress!, testBuyer!, testOrderInfo!);

        OrderItem item = new OrderItem(testCatalogItemId, testProdName, testUnitPrice, testDiscount, testQuantity);
        OrderItem item1 = new OrderItem(testCatalogItemId + 1, testProdName, testUnitPrice, testDiscount, testQuantity);
        OrderItem item2 = new OrderItem(testCatalogItemId + 1, testProdName, testUnitPrice, testDiscount, testQuantity);
        order.OrderItems.Add(item);
        order.OrderItems.Add(item1);
        order.OrderItems.Add(item2);

        var totalTestQuantity = testQuantity * order.OrderItems.Count();
        var result = order.TotalDiscount;
        Assert.Equal(((testUnitPrice*testQuantity)-(testUnitPrice*testQuantity-(testUnitPrice*testDiscount)))*totalTestQuantity, result);
    }

    #endregion


    #region TotalPrice

    [Fact]
    public void ReturnsTotalPriceWithNoItems()
    {
        Order order = new Order(testAddress!, testBuyer!, testOrderInfo!);

        var result = order.TotalPrice;
        Assert.Equal(0, result);
    }

    [Fact]
    public void ReturnsTotalPriceWithOneItem()
    {
        Order order = new Order(testAddress!, testBuyer!, testOrderInfo!);

        OrderItem item = new OrderItem(testCatalogItemId, testProdName, testUnitPrice, testDiscount, testQuantity);
        order.OrderItems.Add(item);

        var result = order.TotalPrice;
        Assert.Equal((testUnitPrice - (testUnitPrice * testDiscount)) * testQuantity * order.OrderItems.Count, result);
    }
    [Fact]
    public void ReturnsTotalPriceWithMultipleItems()
    {
        Order order = new Order(testAddress!, testBuyer!, testOrderInfo!);

        OrderItem item = new OrderItem(testCatalogItemId, testProdName, testUnitPrice, testDiscount, testQuantity);
        OrderItem item1 = new OrderItem(testCatalogItemId + 1, testProdName, testUnitPrice, testDiscount, testQuantity);
        OrderItem item2 = new OrderItem(testCatalogItemId + 1, testProdName, testUnitPrice, testDiscount, testQuantity);
        order.OrderItems.Add(item);
        order.OrderItems.Add(item1);
        order.OrderItems.Add(item2);

        var result = order.TotalPrice;
        Assert.Equal((testUnitPrice - (testUnitPrice * testDiscount)) * testQuantity * order.OrderItems.Count, result);
    }

    #endregion



}