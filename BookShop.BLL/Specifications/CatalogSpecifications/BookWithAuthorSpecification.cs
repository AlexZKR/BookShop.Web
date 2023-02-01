using Ardalis.Specification;
using BookShop.BLL.Entities.Products;

namespace BookShop.BLL.Specifications.CatalogSpecifications;
public class BookWithAuthorSpecification : Specification<Book>
{
    public BookWithAuthorSpecification(int id)
    {
        Query.Where(b => b.Id == id).Include(b => b.Author);
    }
}