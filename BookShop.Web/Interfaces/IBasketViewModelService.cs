using BookShop.BLL.Entities.BasketAggregate;
using BookShop.Web.Models.Basket;

namespace BookShop.Web.Interfaces;

public interface IBasketViewModelService
{
    Task<BasketViewModel> GetOrCreateBasketForUser(string userName);
    Task<int> CountTotalBasketItems(string username);
    Task<BasketViewModel> Map(Basket basket);
    Task<BasketItemViewModel> MapBasketItem(BasketItem item);

}
