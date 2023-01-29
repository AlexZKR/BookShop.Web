using Ardalis.Specification;
using BookShop.BLL.Entities.Enums;
using BookShop.BLL.Entities.Products;

namespace BookShop.BLL.Specifications.CatalogSpecifications;

public class BookCatalogFilterSpecification : Specification<Book>
{
    public BookCatalogFilterSpecification(int? AuthorId, int? cover, int? genre, int? lang)
    {
        Query.Where(i =>
            (!AuthorId.HasValue || i.AuthorId == AuthorId) &&
            (!cover.HasValue || (int)i.Cover == cover) &&
            (!genre.HasValue || (int)i.Genre == genre) &&
            (!lang.HasValue || (int)i.Language == lang))
            .Include(b => b.Author);
    }
}
