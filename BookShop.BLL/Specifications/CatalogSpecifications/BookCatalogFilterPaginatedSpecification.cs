using Ardalis.Specification;
using BookShop.BLL.Entities.Enums;
using BookShop.BLL.Entities.Products;

namespace BookShop.BLL.Specifications.CatalogSpecifications;

public class BookCatalogFilterPaginatedSpecification : Specification<Book>
{
    public BookCatalogFilterPaginatedSpecification(int skip, int take, int? AuthorId, int? cover, int? genre, int? lang) : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }
        Query.Where(i =>
            (!AuthorId.HasValue || i.AuthorId == AuthorId) &&
            (!cover.HasValue || (int)i.Cover == cover) &&
            (!genre.HasValue || (int)i.Genre == genre) &&
            (!lang.HasValue || (int)i.Language == lang))
            .Include(b => b.Author)
            .Skip(skip).Take(take);
    }
}
