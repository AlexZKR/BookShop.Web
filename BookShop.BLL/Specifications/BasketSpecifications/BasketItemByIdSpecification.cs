using Ardalis.Specification;
using BookShop.BLL.Entities.BasketAggregate;

namespace BookShop.BLL.Specifications.BasketSpecifications;

public sealed class BasketItemByIdSpecification : Specification<BasketItem>, ISingleResultSpecification
{
    public BasketItemByIdSpecification(int id)
    {
        Query
            .Where(b => b.Id == id);
    }
}