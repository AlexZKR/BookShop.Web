using Ardalis.Specification;
using BookShop.DAL.Entities.Order;

namespace BookShop.Web.Specifications;

public class CustomerOrdersWithItemsSpecification : Specification<Order>
{
    public CustomerOrdersWithItemsSpecification(string buyerId)
    {
        Query.Where(o => o.Buyer.Id == buyerId)
            .Include(o => o.OrderItems);
        //.ThenInclude(i => i.ItemOrdered);
    }
}