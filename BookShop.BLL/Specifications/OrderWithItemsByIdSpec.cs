using Ardalis.Specification;
using BookShop.DAL.Entities.Order;

namespace BookShop.Web.Specifications;

public class OrderWithItemsByIdSpec : Specification<Order>, ISingleResultSpecification
{
    public OrderWithItemsByIdSpec(int orderId)
    {
        Query
            .Where(order => order.Id == orderId)
            .Include(o => o.OrderItems);
        //.ThenInclude(i => i.ItemOrdered);
    }
}
