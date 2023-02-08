using Ardalis.Specification;
using BookShop.BLL.Entities.Order;

namespace BookShop.BLL.Specifications.OrderSpecifications;

public class UserOrdersWithItemsByUsernameSpecification : Specification<Order>
{
    public UserOrdersWithItemsByUsernameSpecification(string username)
    {
        Query
            .Where(order => order.Buyer.BuyerId == username)
            .Include(o => o.OrderItems);
    }
}