using BookShop.BLL.Entities.Order;
using BookShop.DAL.Data;
using BookShop.UnitTests.Buidlers;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace BookShop.IntegrationTests.Repositories.OrderRepositoryTests;

public class GetById
{
    private readonly AppDbContext _catalogContext;
    private readonly Repository<Order> _orderRepository;
    private OrderBuilder OrderBuilder { get; } = new OrderBuilder();
    private readonly ITestOutputHelper _output;
    public GetById(ITestOutputHelper output)
    {
        _output = output;
        var dbOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestCatalog")
            .Options;
        _catalogContext = new AppDbContext(dbOptions);
        _orderRepository = new Repository<Order>(_catalogContext);
    }

    [Fact]
    public async Task GetsExistingOrder()
    {
        var existingOrder = OrderBuilder.WithDefaultValues();
        _catalogContext.Orders.Add(existingOrder);
        _catalogContext.SaveChanges();
        int orderId = existingOrder.Id;
        _output.WriteLine($"OrderId: {orderId}");

        var orderFromRepo = await _orderRepository.GetByIdAsync(orderId);
        Assert.Equal(OrderBuilder.testProdName, orderFromRepo!.OrderItems[0].ProductName);

        // Note: Using InMemoryDatabase OrderItems is available. Will be null if using SQL DB.
        // Use the OrderWithItemsByIdSpec instead of just GetById to get the full aggregate
        var firstItem = orderFromRepo.OrderItems.FirstOrDefault();
        Assert.Equal(OrderBuilder.testQuantity, firstItem!.Units);
    }
}