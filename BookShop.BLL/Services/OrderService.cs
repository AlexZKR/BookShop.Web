using BookShop.BLL.Entities.BasketAggregate;
using BookShop.BLL.Entities.Order;
using BookShop.BLL.Entities.Products;
using BookShop.BLL.Exceptions;
using BookShop.BLL.Interfaces;
using BookShop.BLL.Specifications.OrderSpecifications;

namespace BookShop.BLL.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> orderRepository;
    private readonly IBasketService basketService;
    private readonly IRepository<BaseProduct> productRepository;
    private readonly IAppLogger<OrderService> logger;
    private readonly IRepository<OrderItem> orderItemsRepository;

    public OrderService(IRepository<Order> orderRepository,
    IBasketService basketService,
    IRepository<BaseProduct> productRepository,
    IAppLogger<OrderService> logger,
    IRepository<OrderItem> orderItemsRepository)
    {
        this.orderRepository = orderRepository;
        this.basketService = basketService;
        this.productRepository = productRepository;
        this.logger = logger;
        this.orderItemsRepository = orderItemsRepository;
    }
    public async Task<Order> CreateOrderAsync(Address address, Buyer buyer, OrderInfo orderInfo)
    {
        if (buyer.BuyerId == null) throw new NotFoundException($"User was not found");
        logger.LogInformation($"Creating order for userId: {buyer.BuyerId}");
        var basket = await basketService.GetBasketAsync(buyer.BuyerId);
        var orderItems = await MapBasketItems(basket.Items);
        var order = new Order(address, buyer, orderInfo)
        {
            OrderItems = orderItems,
        };
        await orderRepository.AddAsync(order);

        //deleting basket
        //items in basket sold++

        //This should be done only after the order is proccessed!!!!!
        //IncSoldOnItems(basket.Items);

        await basketService.DeleteBasketAsync(buyer.BuyerId);

        return order;
    }

    public async Task<List<Order>> GetBuyersOrdersAsync(string username)
    {
        if (username == null) throw new NotFoundException($"User was not found");
        var spec = new UserOrdersWithItemsByUsernameSpecification(username);
        var orderList = await orderRepository.ListAsync(spec);
        return orderList;
    }

    //TODO: make this paged, bool proccessed
    public async Task<List<Order>> GetAllOrdersAsync()
    {
        var spec = new AllOrdersWithItemsSpecification();
        return await orderRepository.ListAsync(spec);
        // var spec = new OrdersByProccessedSpecification(false);
        // return await orderRepository.ListAsync(spec);
    }

    private async Task<List<OrderItem>> MapBasketItems(IReadOnlyCollection<BasketItem> basketItems)
    {
        var list = new List<OrderItem>();
        foreach (var item in basketItems)
        {
            var product = await productRepository.GetByIdAsync(item.ProductId);
            if (product == null) throw new Exceptions.NotFoundException($"Item with id {item.ProductId} not found in db");

            var orderItem = new OrderItem(item.Id,
                                          product.Name,
                                          item.FullPrice,
                                          item.Discount,
                                          item.Quantity);
            list.Add(orderItem);

            // if (item is Book)
            // {
            //     orderItem.AddInfo = $"Автор: {(item as Book)!.Author.Name}";
            // }
        }
        return list;
    }
    private async void IncSoldOnItems(IReadOnlyCollection<BasketItem> basketItems)
    {

        foreach (var item in basketItems)
        {
            var product = await productRepository.GetByIdAsync(item.ProductId);
            if (product != null)
            {
                product.Sold++;
                await productRepository.UpdateAsync(product);
            }
            else
                throw new NotFoundException($"Product with id {item.ProductId} was not fount");
        }
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        var spec = new OrderWithItemsByIdSpecification(id);
        var order = await orderRepository.FirstOrDefaultAsync(spec);
        if (order == null) throw new NotFoundException($"Order with id {id} was not found.");
        return order;
    }

    public async Task<Order> GetOrderByUsernameAsync(string username)
    {
        var spec = new UserOrdersWithItemsByUsernameSpecification(username);
        var order = await orderRepository.FirstOrDefaultAsync(spec);
        if (order == null) throw new NotFoundException($"Order with id {username} was not found.");
        return order;
    }


    public async Task<List<Order>> GetAllBuyersAsync()
    {
        var spec = new BuyersOnlySpecification();
        var buyers = await orderRepository.ListAsync(spec);
        //set non proccessed orders count for api notification
        foreach (var item in buyers)
        {
            if(item.Buyer.BuyerId == null) throw new NotFoundException($"User was not found");
            var countSpec = new UnproccessedByUserNameSpecification(item.Buyer.BuyerId);
            item.Buyer.UnproccessedOrdersCount = await orderRepository.CountAsync(countSpec);
        }
        return buyers;
    }

    public async Task<Order> ApproveOrderByIdAsync(int id)
    {
        var order = await GetOrderByIdAsync(id);
        order.IsInProcess = false;
        await orderRepository.UpdateAsync(order);
        return order;
    }
}