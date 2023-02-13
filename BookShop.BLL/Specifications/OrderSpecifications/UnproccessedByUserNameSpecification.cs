using Ardalis.Specification;
using BookShop.BLL.Entities.Order;

namespace BookShop.BLL.Specifications.OrderSpecifications;
//TODO: make this paged
public class UnproccessedByUserNameSpecification : Specification<Order>
{
    public UnproccessedByUserNameSpecification(string username)
    {
        Query.Where(o =>
        (o.IsInProcess == true) && (o.Buyer.BuyerId == username));
    }
}