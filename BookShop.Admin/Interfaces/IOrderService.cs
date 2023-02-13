namespace BookShop.Admin.Interfaces;

public interface IOrderService : IBaseService
{
    Task<T> GetAllBuyersAsync<T>();
    Task<T> GetUserWithOrdersByIdAsync<T>(string id);


}