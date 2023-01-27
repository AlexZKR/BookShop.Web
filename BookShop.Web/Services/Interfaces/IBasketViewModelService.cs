
using BookShop.DAL.Entities.Basket;
using BookShop.Web.Models;

namespace BookShop.Web.Services.Interfaces;

public interface IBasketViewModelService
{
    Task<BasketViewModel> GetOrCreateBasketForUser(string userName);
    Task<int> CountTotalBasketItems(string username);
    Task<BasketViewModel> Map(Basket basket);
}
