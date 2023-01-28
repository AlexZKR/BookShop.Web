using Ardalis.Specification;
using BookShop.BLL.Entities.Products;

namespace BookShop.BLL.Specifications.CatalogSpecifications;
public class BookCatalogSearchQuerySpecification : Specification<Book>
{
    public BookCatalogSearchQuerySpecification(string searchQuery)
    {
        Query.Where(b =>
        b.Name.ToLower().Contains(searchQuery.ToLower()) ||
        b.Author.Name!.ToLower()!.Contains(searchQuery.ToLower()));
    }
}
