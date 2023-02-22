using Ardalis.Specification;
using BookShop.BLL.Entities.Products;

namespace BookShop.BLL.Specifications.CatalogSpecifications;

public class BookPaginatedSpecification : Specification<Book>
{
    public BookPaginatedSpecification(int skip, int take) : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }
        Query.Include(b => b.Author).Skip(skip).Take(take);
    }
}
