using BookShop.Web.Models.Order;

namespace BookShop.Web.Interfaces
{
    public interface ICheckOutViewModelService
    {
        Task<CheckOutViewModel> CreateCheckOutVMAsync(string username);
    }
}