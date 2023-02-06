using BookShop.BLL.Entities.Basket;
using BookShop.BLL.Entities.Order;
using BookShop.BLL.Entities.Products;
using BookShop.BLL.Exceptions;
using BookShop.BLL.Interfaces;

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
        if (buyer.BuyerId == null) throw new NotFoundInDbException($"User was not found in db.");
        logger.LogInformation($"Creating order for userId: {buyer.BuyerId}");
        var basket = await basketService.GetBasketAsync(buyer.BuyerId);
        var orderItems = await MapBasketItems(basket.Items);
        var order = new Order(address, buyer, orderInfo)
        {
            OrderItems = orderItems,
        };
        await orderRepository.AddAsync(order);
        //configuring order items
        // order.OrderItems.Select(i => i.OrderId = order.Id);

        //await orderItemsRepository.AddRangeAsync(order.OrderItems);

        //deleting basket
        //TODO items in basket sold++

        await basketService.DeleteBasketAsync(buyer.BuyerId);

        return order;
    }
    // public async Task<Order> CreateOrderAsync(double totalPrice, double discountSize, string orderComment, string buyerId, string firstName, string lastName, string phoneNumber, string email, string street, string city, string postCode, int region, int paymentType, int deliveryType)
    // {
    //     var basket = await basketService.GetBasketAsync(buyerId);

    //     var order = new Order()
    //     {
    //         OrderItems = MapBasketItems(await basketService.GetBasketItemsAsync(buyerId)),

    //         TotalPrice = totalPrice,
    //         DiscountSize = discountSize,
    //         OrderComment = orderComment,
    //         BuyerId = buyerId,

    //         FirstName = firstName,
    //         LastName = lastName,
    //         Email = email,
    //         PhoneNumber = phoneNumber,


    //         Region = (Region)region,
    //         City = city,
    //         Street = street,
    //         PostCode = postCode,

    //         PaymentType = (PaymentType)paymentType,
    //         DeliveryType = (DeliveryType)deliveryType

    //     };

    //     await orderRepository.AddAsync(order);
    //     return order;
    // }

    private async Task<List<OrderItem>> MapBasketItems(IReadOnlyCollection<BasketItem> basketItems)
    {
        var list = new List<OrderItem>();
        foreach (var item in basketItems)
        {
            var product = await productRepository.GetByIdAsync(item.ProductId);
            if (product == null) throw new Exceptions.NotFoundInDbException($"Item with id {item.ProductId} not found in db");

            var orderItem = new OrderItem(item.Id,
                                          product.Name,
                                          item.FullPrice,
                                          item.DiscountedPrice,
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


}