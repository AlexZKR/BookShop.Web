using Ardalis.Specification;
using BookShop.BLL.Entities.Enums;
using BookShop.BLL.Entities.Products;
using BookShop.BLL.Infrastructure;

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
            (!(cover.HasValue && cover != 0) || (int)i.Cover == cover) &&
            (!(genre.HasValue && genre != 0) || (int)i.Genre == genre) &&
            (!(lang.HasValue && lang != 0) || (int)i.Language == lang))
            .Include(b => b.Author)
            .Skip(skip).Take(take);
    }
}
