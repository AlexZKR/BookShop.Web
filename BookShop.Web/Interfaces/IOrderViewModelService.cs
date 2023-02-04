using BookShop.Web.Models.Order;

namespace BookShop.Web.Interfaces
{
    public interface IOrderViewModelService
    {
        Task<OrderViewModel> CreateOrderVMAsync(string username);
    }
}