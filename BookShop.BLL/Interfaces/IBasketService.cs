using Ardalis.Result;
using BookShop.BLL.Entities.Basket;

namespace BookShop.BLL.Interfaces;

public interface IBasketService
{
    Task TransferBasketAsync(string anonymousId, string userName);
    Task<Basket> AddItemToBasket(string username, int catalogItemId, double price, int quantity = 1);
    void RemoveItemFromBasket(string username, int id);
    Task<Result<Basket>> SetQuantities(int basketId, Dictionary<string, int> quantities);
    void UpDownQuantity(string username, int itemId, string mode);
    Task DeleteBasketAsync(int basketId);
}
