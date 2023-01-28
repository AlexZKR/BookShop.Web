using Ardalis.Specification;
using BookShop.BLL.Entities.Enums;
using BookShop.BLL.Entities.Products;

namespace BookShop.BLL.Specifications.CatalogSpecifications;

public class BookCatalogFilterSpecification : Specification<Book>
{
    public BookCatalogFilterSpecification(int? AuthorId, Cover? cover, Genre? genre, Language? lang)
    {
        Query.Where(i =>
            (!AuthorId.HasValue || i.AuthorId == AuthorId) &&
            (!cover.HasValue || i.Cover == cover) &&
            (!genre.HasValue || i.Genre == genre) &&
            (!lang.HasValue || i.Language == lang))
            .Include(b => b.Author);
    }
}
