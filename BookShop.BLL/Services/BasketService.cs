using Ardalis.GuardClauses;
using Ardalis.Result;
using BookShop.BLL.Entities.Basket;
using BookShop.BLL.Interfaces;
using BookShop.BLL.Specifications;

namespace BookShop.BLL.Services;

public class BasketService : IBasketService
{
    private readonly IRepository<Basket> basketRepository;
    private readonly IAppLogger<BasketService> logger;

    public BasketService(IRepository<Basket> basketRepository,
        IAppLogger<BasketService> logger)
    {
        this.basketRepository = basketRepository;
        this.logger = logger;
    }

    public async Task<Basket> AddItemToBasket(string username, int catalogItemId, double price, int quantity = 1)
    {
        var basketSpec = new BasketWithItemsSpecification(username);
        var basket = await basketRepository.FirstOrDefaultAsync(basketSpec);

        if (basket == null)
        {
            basket = new Basket(username);
            await basketRepository.AddAsync(basket);
        }

        basket.AddItem(catalogItemId, price, quantity);

        await basketRepository.UpdateAsync(basket);
        return basket;
    }

    public async void RemoveItemFromBasket(string username, int id)
    {
        var basketSpec = new BasketWithItemsSpecification(username);
        var basket = await basketRepository.FirstOrDefaultAsync(basketSpec);

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
        var basket = await basketRepository.GetByIdAsync(basketId);
        Guard.Against.Null(basket, nameof(basket));
        await basketRepository.DeleteAsync(basket);
    }

    public async Task<Result<Basket>> SetQuantities(int basketId, Dictionary<string, int> quantities)
    {
        var basketSpec = new BasketWithItemsSpecification(basketId);
        var basket = await basketRepository.FirstOrDefaultAsync(basketSpec);


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
        var anonymousBasketSpec = new BasketWithItemsSpecification(anonymousId);
        var anonymousBasket = await basketRepository.FirstOrDefaultAsync(anonymousBasketSpec);
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
            userBasket.AddItem(item.ProductId, item.UnitPrice, item.Quantity);
        }

        await basketRepository.UpdateAsync(userBasket);
        await basketRepository.DeleteAsync(anonymousBasket);
    }
}