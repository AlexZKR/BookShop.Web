using Ardalis.Specification;
using BookShop.BLL.Entities.Order;

namespace BookShop.BLL.Specifications.OrderSpecifications;
//TODO: make this paged
public class BuyersOnlySpecification : Specification<Order>
{
    public BuyersOnlySpecification()
    {
        Query.Include(o => o.Buyer);
    }
}