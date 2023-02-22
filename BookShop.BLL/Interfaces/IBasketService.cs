using Ardalis.Result;
using BookShop.BLL.Entities.BasketAggregate;
using BookShop.BLL.Entities.Products;

namespace BookShop.BLL.Interfaces;

public interface IBasketService
{
    Task TransferBasketAsync(string anonymousId, string userName);
    Task<Basket> AddItemToBasket(string username, int catalogItemId, double price, double discount, int quantity = 1);
    void RemoveItemFromBasket(string username, int id);
    Task<Result<Basket>> SetQuantities(int basketId, Dictionary<string, int> quantities);
    Task<BasketItem> UpDownQuantity(string username, int itemId, string mode);
    Task DeleteBasketAsync(int basketId);
    Task DeleteBasketAsync(string buyerId);
    Task<Basket> GetBasketAsync(int basketId);
    Task<Basket> GetBasketAsync(string username);
    Task<List<BaseProduct>> GetBasketItemsAsync(string username);
    Task<List<BaseProduct>> GetBasketItemsAsync(int basketId);
    Task<BasketItem> GetBasketItemAsync(int id);
    Task<bool> CheckIfEmpty(int basketId);
}
