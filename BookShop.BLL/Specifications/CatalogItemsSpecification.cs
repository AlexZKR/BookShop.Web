using Ardalis.Specification;
using BookShop.BLL.Entities.Products;

namespace BookShop.BLL.Specifications;

public class CatalogItemsSpecification : Specification<Book>
{
    public CatalogItemsSpecification(params int[] ids)
    {
        Query.Where(c => ids.Contains(c.Id));
    }
}
