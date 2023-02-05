using Ardalis.GuardClauses;
using Ardalis.Result;
using BookShop.BLL.Entities.Basket;
using BookShop.BLL.Entities.Products;
using BookShop.BLL.Exceptions;
using BookShop.BLL.Interfaces;
using BookShop.BLL.Specifications;

namespace BookShop.BLL.Services;

public class BasketService : IBasketService
{
    private readonly IRepository<Basket> basketRepository;
    private readonly IAppLogger<BasketService> logger;
    private readonly IRepository<BaseProduct> productRepository;

    public BasketService(IRepository<Basket> basketRepository,
        IAppLogger<BasketService> logger,
        IRepository<BaseProduct> productRepository)
    {
        this.basketRepository = basketRepository;
        this.logger = logger;
        this.productRepository = productRepository;
    }

    public async Task<Basket> AddItemToBasket(string username, int catalogItemId, double price, double discount, int quantity = 1)
    {
        var basketSpec = new BasketWithItemsSpecification(username);
        var basket = await basketRepository.FirstOrDefaultAsync(basketSpec);

        if (basket == null)
        {
            basket = new Basket(username);
            await basketRepository.AddAsync(basket);
        }

        basket.AddItem(catalogItemId, price, discount, quantity);

        await basketRepository.UpdateAsync(basket);
        return basket;
    }

    public async void UpDownQuantity(string username, int itemId, string mode)
    {
        var basket = await GetBasketAsync(username);

        if (basket != null)
        {
            var item = basket.Items.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                switch (mode)
                {
                    case "add":
                        item.AddQuantity(1);
                        break;
                    case "sub":
                        item.DecreaseQuantity(1);
                        if (item.Quantity == 0)
                            basket.RemoveEmptyItems();
                        break;
                }
                await basketRepository.UpdateAsync(basket);

            }
        }
    }

    public async void RemoveItemFromBasket(string username, int id)
    {
        var basket = await GetBasketAsync(username);

        Guard.Against.Null(basket, nameof(basket));
        var item = basket.Items.Where(i => i.Id == id).FirstOrDefault();

        if (item != null)
        {
            item.SetQuantity(0);
            basket.RemoveEmptyItems();
            await basketRepository.UpdateAsync(basket);
        }
    }
    public async Task DeleteBasketAsync(int basketId)
    {
        var basket = await GetBasketAsync(basketId);
        Guard.Against.Null(basket, nameof(basket));
        await basketRepository.DeleteAsync(basket);
    }

    public async Task<Result<Basket>> SetQuantities(int basketId, Dictionary<string, int> quantities)
    {
        var basket = await GetBasketAsync(basketId);


        if (basket == null) return Result<Basket>.NotFound();

        foreach (var item in basket.Items)
        {
            if (quantities.TryGetValue(item.Id.ToString(), out var quantity))
            {
                if (logger != null) logger.LogInformation($"Updating quantity of item ID:{item.Id} to {quantity}.");
                item.SetQuantity(quantity);
            }
        }
        basket.RemoveEmptyItems();
        await basketRepository.UpdateAsync(basket);
        return basket;
    }

    public async Task TransferBasketAsync(string anonymousId, string userName)
    {
        var anonymousBasket = await GetBasketAsync(anonymousId);
        if (anonymousBasket == null) return;


        var userBasketSpec = new BasketWithItemsSpecification(userName);
        var userBasket = await basketRepository.FirstOrDefaultAsync(userBasketSpec);

        if (userBasket == null)
        {
            userBasket = new Basket(userName);
            await basketRepository.AddAsync(userBasket);
        }

        foreach (var item in anonymousBasket.Items)
        {
            userBasket.AddItem(item.ProductId, item.FullPrice, item.Quantity);
        }

        await basketRepository.UpdateAsync(userBasket);
        await basketRepository.DeleteAsync(anonymousBasket);
    }

    public async Task<Basket> GetBasketAsync(string username)
    {
        var basketSpec = new BasketWithItemsSpecification(username);
        var basket = await basketRepository.FirstOrDefaultAsync(basketSpec);
        if (basket != null)
            return basket;
        throw new BasketNotFoundException(username);
    }
    public async Task<Basket> GetBasketAsync(int busketId)
    {
        var basketSpec = new BasketWithItemsSpecification(busketId);
        var basket = await basketRepository.FirstOrDefaultAsync(basketSpec);
        if (basket != null)
            return basket;
        throw new BasketNotFoundException(busketId);
    }

    public async Task<List<BaseProduct>> GetBasketItemsAsync(string username)
    {
        var basket = await GetBasketAsync(username);
        var list = new List<BaseProduct>();
        foreach (var item in basket.Items)
        {
            var product = await productRepository.GetByIdAsync(item.ProductId);
            if (product != null)
                list.Add(product);
            else
                throw new ProductNotFoundException(item.ProductId);
        }
        return list;
    }
    public async Task<List<BaseProduct>> GetBasketItemsAsync(int busketId)
    {
        var basket = await GetBasketAsync(busketId);
        var list = new List<BaseProduct>();
        foreach (var item in basket.Items)
        {
            var product = await productRepository.GetByIdAsync(item.ProductId);
            if (product != null)
                list.Add(product);
            else
                throw new ProductNotFoundException(item.ProductId);
        }
        return list;
    }
}