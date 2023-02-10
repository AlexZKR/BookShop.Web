using BookShop.BLL.Entities.Products;

namespace BookShop.BLL.Interfaces;

public interface IBookCatalogService
{
    Task<List<Book>> GetCatalogItems(string username, int pageIndex, int itemsPage, string? searchQuery, int? AuthorId, int? cover, int? genre, int? lang);
    Task<List<Book>> GetTopSoldItems(int quantity, string username);
    Task<IEnumerable<Author>> GetAuthors();


}
