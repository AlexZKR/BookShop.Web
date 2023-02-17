using BookShop.BLL.Entities.Basket;
using BookShop.BLL.Entities.Products;
using BookShop.BLL.Interfaces;
using BookShop.Web.Interfaces;
using BookShop.BLL.Specifications;
using BookShop.BLL.Specifications.CatalogSpecifications;
using BookShop.Web.Models.Basket;
using AutoMapper;

namespace BookShop.Web.Services;

public class BasketViewModelService : IBasketViewModelService
{
    private readonly IRepository<Basket> basketRepository;
    private readonly IImageService uriComposer;
    private readonly IBasketQueryService basketQueryService;
    private readonly IMapper mapper;
    private readonly IRepository<BaseProduct> itemRepository;

    public BasketViewModelService(IRepository<Basket> basketRepository,
        IRepository<BaseProduct> itemRepository,
        IImageService uriComposer,
        IBasketQueryService basketQueryService,
        IMapper mapper)
    {
        this.basketRepository = basketRepository;
        this.uriComposer = uriComposer;
        this.basketQueryService = basketQueryService;
        this.mapper = mapper;
        this.itemRepository = itemRepository;
    }

    public async Task<BasketViewModel> GetOrCreateBasketForUser(string userName)
    {
        var basketSpec = new BasketWithItemsSpecification(userName);
        var basket = (await basketRepository.FirstOrDefaultAsync(basketSpec));

        if (basket == null)
        {
            return await CreateBasketForUser(userName);
        }
        var viewModel = await Map(basket);
        viewModel.TotalItems = await CountTotalBasketItems(userName);
        return viewModel;
    }

    private async Task<BasketViewModel> CreateBasketForUser(string userId)
    {
        var basket = new Basket(userId);
        await basketRepository.AddAsync(basket);

        return new BasketViewModel()
        {
            BuyerId = basket.BuyerId,
            Id = basket.Id,
        };
    }

    private async Task<List<BasketItemViewModel>> GetBasketItems(IReadOnlyCollection<BasketItem> basketItems)
    {
        var items = new List<BasketItemViewModel>();
        foreach(var item in basketItems)
        {
            items.Add(await MapBasketItem(item));
        }
        return items;
    }

    public async Task<BasketItemViewModel> MapBasketItem(BasketItem item)
    {
        var viewModel = mapper.Map<BasketItem, BasketItemViewModel>(item);
        viewModel.PictureUrl = await uriComposer.ComposePicUriById(viewModel.ProductId);
        return viewModel;
    }

    public async Task<BasketViewModel> Map(Basket basket)
    {
        return new BasketViewModel()
        {
            BuyerId = basket.BuyerId,
            Id = basket.Id,
            TotalDiscount = basket.TotalDiscount,
            TotalPrice = basket.TotalPrice,
            Items = await GetBasketItems(basket.Items)
        };
    }

    public async Task<int> CountTotalBasketItems(string username)
    {
        var counter = await basketQueryService.CountTotalBasketItemsAsync(username);

        return counter;
    }
}
