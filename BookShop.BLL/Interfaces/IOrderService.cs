using BookShop.BLL.Entities.Order;

namespace BookShop.BLL.Interfaces;

public interface IOrderService
{
    //Task<Order> CreateOrderAsync(double totalPrice, double discountSize, string orderComment, string buyerId, string firstName, string lastName, string phoneNumber, string email, string street, string city, string postCode, int region, int paymentType, int deliveryType);
    Task<Order> CreateOrderAsync(Address address, Buyer buyer, OrderInfo orderInfo);
    Task<List<Order>> GetUserOrdersAsync(string username);
}