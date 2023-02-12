using Ardalis.Specification;
using BookShop.BLL.Entities.Order;

namespace BookShop.BLL.Specifications.OrderSpecifications;
//TODO: make this paged
public class AllOrdersWithItemsSpecification : Specification<Order>
{
    public AllOrdersWithItemsSpecification()
    {
        Query
            .Include(o => o.OrderItems);
    }
}