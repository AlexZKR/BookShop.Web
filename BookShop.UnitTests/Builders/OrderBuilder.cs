using BookShop.BLL.Entities.Order;

namespace BookShop.UnitTests.Buidlers;

public class OrderBuilder
{
    private Order _order;
    public Address? testAddress = null;
    public Buyer? testBuyer = null;
    public OrderInfo? testOrderInfo = null;

    private readonly int testCatalogItemId = 123;
    public readonly string testProdName = "testName";
    private readonly double testUnitPrice = 10;
    private readonly double testDiscount = 0.1;
    public readonly int testQuantity = 2;


    public OrderBuilder()
    {
        _order = WithDefaultValues();
    }

    public Order Build()
    {
        return _order;
    }

    public Order WithDefaultValues()
    {
        OrderItem item = new OrderItem(testCatalogItemId, testProdName, testUnitPrice, testDiscount, testQuantity);
        _order = new Order(testAddress!, testBuyer!, testOrderInfo!);
        _order.OrderItems.Add(item);
        return _order;
    }

    public Order WithNoItems()
    {
        OrderItem item = new OrderItem(testCatalogItemId, testProdName, testUnitPrice, testDiscount, testQuantity);
        return _order;
    }

}