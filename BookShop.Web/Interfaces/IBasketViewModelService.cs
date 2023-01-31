using BookShop.BLL.Entities.Basket;
using BookShop.Web.Models.Basket;

namespace BookShop.Web.Interfaces;

public interface IBasketViewModelService
{
    Task<BasketViewModel> GetOrCreateBasketForUser(string userName);
    Task<int> CountTotalBasketItems(string username);
    Task<BasketViewModel> Map(Basket basket);
}
