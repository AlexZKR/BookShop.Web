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

    public OrderService(IRepository<Order> orderRepository,
    IBasketService basketService,
    IRepository<BaseProduct> productRepository)
    {
        this.orderRepository = orderRepository;
        this.basketService = basketService;
        this.productRepository = productRepository;
    }
    public async Task<Order> CreateOrderAsync(string username)
    {
        // var basket = await basketService.GetBasketAsync(username);
        // var orderItems = await MapBasketItems(basket.Items);
        // // var order = new Order
        // // {
        // //     OrderItems = orderItems,

        // // };
        // // await orderRepository.AddAsync(order);
        // return new Order(new Address(), new Buyer(), new OrderInfo());
        return null;
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
            if (product == null) throw new ProductNotFoundException(item.ProductId);
            var orderItem = new OrderItem(item.Id,
                                          product.Name,
                                          item.FullPrice,
                                          item.DiscountedPrice,
                                          item.Discount,
                                          item.Quantity);

            // if (item is Book)
            // {
            //     orderItem.AddInfo = $"Автор: {(item as Book)!.Author.Name}";
            // }
        }
        return list;
    }


}