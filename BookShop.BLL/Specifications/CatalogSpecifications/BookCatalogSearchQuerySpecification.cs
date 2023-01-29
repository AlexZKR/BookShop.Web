using Ardalis.Specification;
using BookShop.BLL.Entities.Products;

namespace BookShop.BLL.Specifications.CatalogSpecifications;
public class BookCatalogSearchQuerySpecification : Specification<Book>
{
    public BookCatalogSearchQuerySpecification(string searchQuery)
    {
        Query.Include(b => b.Author).Where(b =>
        (b.Name.ToLower().Contains(searchQuery.ToLower())) || (b.Author.Name!.ToLower()!.Contains(searchQuery.ToLower()))).OrderBy(n => n.Name);
        // || (b.Author.Name!.ToLower()!.Contains(searchQuery.ToLower())));
        // Query.Where(b =>
        // b.Name.ToLower().Contains(searchQuery.ToLower()));
        // Query.Where(b =>
        // b.Id.ToString() == searchQuery);
    }
}
