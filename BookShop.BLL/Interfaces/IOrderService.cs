using BookShop.BLL.Entities.Order;

namespace BookShop.BLL.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(Address address, Buyer buyer, OrderInfo orderInfo);
    Task<List<Order>> GetUserOrdersAsync(string username);
    //TODO: make this paged, bool proccessed
    Task<List<Order>> GetAllOrdersAsync();
    Task<Order> GetOrderByIdAsync(int id);
    Task<Order> GetOrderByUsernameAsync(string username);
}