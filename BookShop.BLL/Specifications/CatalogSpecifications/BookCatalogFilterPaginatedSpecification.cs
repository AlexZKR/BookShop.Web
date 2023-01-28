using Ardalis.Specification;
using BookShop.BLL.Entities.Enums;
using BookShop.BLL.Entities.Products;

namespace BookShop.BLL.Specifications.CatalogSpecifications;

public class BookCatalogFilterPaginatedSpecification : Specification<Book>
{
    public BookCatalogFilterPaginatedSpecification(int skip, int take, int? AuthorId, Cover? cover, Genre? genre, Language? lang) : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }
        Query.Where(i =>
            (!AuthorId.HasValue || i.AuthorId == AuthorId) &&
            (!cover.HasValue || i.Cover == cover) &&
            (!genre.HasValue || i.Genre == genre) &&
            (!lang.HasValue || i.Language == lang))
            .Include(b => b.Author)
            .Skip(skip).Take(take);
    }
}
