
using BookShop.Web.Models.Order;

namespace BookShop.Web.Services;

public interface IOrderViewModelService
{
    Task<OrderViewModel> CreateVM();
}