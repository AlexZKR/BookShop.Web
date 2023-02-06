using Ardalis.Specification;
using BookShop.BLL.Entities.Order;

namespace BookShop.BLL.Specifications.OrderSpecifications;

public class OrderWithItemsByIdSpecification : Specification<Order>
{
    public OrderWithItemsByIdSpecification(int orderId)
    {
        Query
            .Where(order => order.Id == orderId)
            .Include(o => o.OrderItems);
    }
}