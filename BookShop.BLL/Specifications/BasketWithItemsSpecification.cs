using Ardalis.Specification;
using BookShop.BLL.Entities.Basket;

namespace BookShop.BLL.Specifications;

public sealed class BasketWithItemsSpecification : Specification<Basket>, ISingleResultSpecification
{
    public BasketWithItemsSpecification(int basketId)
    {
        Query
            .Where(b => b.Id == basketId)
            .Include(b => b.Items);
    }

    public BasketWithItemsSpecification(string username)
    {
        Query
            .Where(b => b.BuyerId == username)
            .Include(b => b.Items);
    }
}
