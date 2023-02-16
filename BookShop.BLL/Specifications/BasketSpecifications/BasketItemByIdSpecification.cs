using Ardalis.Specification;
using BookShop.BLL.Entities.Basket;

namespace BookShop.BLL.Specifications.BasketSpecifications;

public sealed class BasketItemByIdSpecification : Specification<BasketItem>, ISingleResultSpecification
{
    public BasketItemByIdSpecification(int id)
    {
        Query
            .Where(b => b.Id == id);
    }
}