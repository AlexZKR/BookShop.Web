namespace BookShop.Admin.Interfaces;

public interface IOrderService : IBaseService
{
    Task<T> GetAllOrdersAsync<T>();
    Task<T> GetOrderByIdAsync<T>(int id);


}