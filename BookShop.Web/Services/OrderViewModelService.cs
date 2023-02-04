using Ardalis.GuardClauses;
using BookShop.BLL.Entities.Basket;
using BookShop.BLL.Entities.Enums;
using BookShop.BLL.Entities.Products;
using BookShop.BLL.Extensions;
using BookShop.BLL.Interfaces;
using BookShop.BLL.Specifications;
using BookShop.BLL.Specifications.CatalogSpecifications;
using BookShop.DAL.Entities.Order;
using BookShop.Web.Infrastructure;
using BookShop.Web.Interfaces;
using BookShop.Web.Models.Order;

namespace BookShop.Web.Services;

public class OrderViewModelService : IOrderViewModelService
{
    private readonly IUriComposer uriComposer;
    private readonly IRepository<Basket> basketRepository;
    private readonly IRepository<Book> itemRepository;

    public OrderViewModelService(IRepository<Basket> basketRepository,
        IRepository<Book> itemRepository,
        IRepository<Order> orderRepository,
        IUriComposer uriComposer)
    {
        this.basketRepository = basketRepository;
        this.itemRepository = itemRepository;
        this.uriComposer = uriComposer;
    }

    public async Task<OrderViewModel> CreateOrderVMAsync(string username)
    {
        var basketSpec = new BasketWithItemsSpecification(username);
        var basket = await basketRepository.FirstOrDefaultAsync(basketSpec);

        Guard.Against.Null(basket, nameof(basket));
        Guard.Against.EmptyBasketOnCheckout(basket.Items);

        var catalogItemsSpecification = new BookCatalogItemsSpecification(basket.Items.Select(item => item.ProductId).ToArray());
        var catalogItems = await itemRepository.ListAsync(catalogItemsSpecification);


        var vm = new OrderViewModel()
        {
            BuyerId = username,
            OrderDate = DateTimeOffset.Now,
            TotalPrice = basket.Items.Sum(i => i.UnitPrice),
            TotalItems = basket.TotalItems,

            Regions = EnumHelper<Region>.GetStaticDataFromEnum(Region.Minsk).ToList(),
            PaymentTypes = EnumHelper<PaymentType>.GetStaticDataFromEnum(PaymentType.Cash).ToList(),
            DeliveryTypes = EnumHelper<DeliveryType>.GetStaticDataFromEnum(DeliveryType.FreeShipment).ToList(),

            OrderItems = catalogItems.Select(i => new OrderItemViewModel()
            {
                ProductId = i.Id,
                ProductName = i.Name,
                FullPrice = i.Price,
                DiscountedPrice = i.DiscountedPrice,
                Discount = i.Discount,
                Units = basket.Items.First(i => i.Id == i.Id).Quantity,
                PictureUrl = uriComposer.ComposePicUri(i.ImagePath),
                AddInfo = i.Author.Name

            }).ToList(),
        };



        return vm;
        // var items = basket.Items.Select(basketItem =>
        // {
        //     var catalogItem = catalogItems.First(c => c.Id == basketItem.ProductId);
        //     var itemOrdered = new CatalogItemOrdered(catalogItem.Id, catalogItem.Name, _uriComposer.ComposePicUri(catalogItem.ImagePath));
        //     var orderItem = new OrderItem(itemOrdered, basketItem.UnitPrice, basketItem.Quantity);
        //     return orderItem;
        // }).ToList();

        //var order = new Order(basket.BuyerId, new Address(), items);

        //await _orderRepository.AddAsync(order);
    }



}
