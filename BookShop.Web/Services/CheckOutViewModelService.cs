using Ardalis.GuardClauses;
using BookShop.BLL.Entities.Basket;
using BookShop.BLL.Entities.Enums;
using BookShop.BLL.Entities.Order;
using BookShop.BLL.Entities.Products;
using BookShop.BLL.Extensions;
using BookShop.BLL.Interfaces;
using BookShop.BLL.Specifications;
using BookShop.BLL.Specifications.CatalogSpecifications;
using BookShop.Web.Infrastructure;
using BookShop.Web.Interfaces;
using BookShop.Web.Models.Order;

namespace BookShop.Web.Services;

public class CheckOutViewModelService : ICheckOutViewModelService
{
    private readonly IUriComposer uriComposer;
    private readonly IRepository<Basket> basketRepository;
    private readonly IRepository<BaseProduct> itemRepository;

    public CheckOutViewModelService(IRepository<Basket> basketRepository,
        IRepository<BaseProduct> itemRepository,
        IRepository<Order> orderRepository,
        IUriComposer uriComposer)
    {
        this.basketRepository = basketRepository;
        this.itemRepository = itemRepository;
        this.uriComposer = uriComposer;
    }

    public async Task<CheckOutViewModel> CreateOrderVMAsync(string username)
    {
        var basketSpec = new BasketWithItemsSpecification(username);
        var basket = await basketRepository.FirstOrDefaultAsync(basketSpec);

        Guard.Against.Null(basket, nameof(basket));
        Guard.Against.EmptyBasketOnCheckout(basket.Items);

        var catalogItemsSpecification = new BookCatalogItemsSpecification(basket.Items.Select(item => item.ProductId).ToArray());
        var catalogItems = await itemRepository.ListAsync(catalogItemsSpecification);


        var vm = new CheckOutViewModel()
        {
            BuyerId = username,
            TotalPrice = basket.Items.Sum(i => i.FullPrice),
            TotalItems = basket.TotalItems,

            Regions = EnumHelper<Region>.GetStaticDataFromEnum(Region.Minsk).ToList(),
            PaymentTypes = EnumHelper<PaymentType>.GetStaticDataFromEnum(PaymentType.Cash).ToList(),
            DeliveryTypes = EnumHelper<DeliveryType>.GetStaticDataFromEnum(DeliveryType.FreeShipment).ToList(),

            OrderItems = catalogItems.Select(i => new OrderItemViewModel()
            {
                ProductId = i.Id,
                ProductName = i.Name,
                FullPrice = i.FullPrice,
                DiscountedPrice = i.DiscountedPrice,
                Discount = i.Discount,
                Units = basket.Items.First(i => i.Id == i.Id).Quantity,
                PictureUrl = uriComposer.ComposePicUri(i.ImagePath),
                //AddInfo = i.Author.Name

            }).ToList(),
        };

        return vm;

    }



}
