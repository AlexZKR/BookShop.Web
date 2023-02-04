using Ardalis.Specification;
using BookShop.BLL.Entities.Products;

namespace BookShop.BLL.Specifications.CatalogSpecifications;

public class BookCatalogItemsSpecification : Specification<Book>
{
    public BookCatalogItemsSpecification(params int[] ids)
    {
        Query.Where(c => ids.Contains(c.Id)).Include(a => a.Author);
    }
}
