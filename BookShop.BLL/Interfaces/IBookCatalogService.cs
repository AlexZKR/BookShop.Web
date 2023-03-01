using BookShop.BLL.Entities.Products;

namespace BookShop.BLL.Interfaces;

public interface IBookCatalogService
{
    Task<List<Book>> GetCatalogItems(string username, string? searchQuery, int pageIndex = 0, int itemsPage = SD.ITEMS_PER_PAGE, int? AuthorId = 0, int? cover = null, int? genre = null, int? lang = null);
    Task<int> TotalItemsCountAsync(string? searchQuery, int? AuthorId, int? cover, int? genre, int? lang, int pageIndex = 0, int itemsPage = SD.ITEMS_PER_PAGE);
    Task<List<Book>> GetTopSoldItems(int quantity, string username);
    Task<IEnumerable<Author>> GetAuthors();

    Task<int> CountBooksAsync();
    Task<Book> GetBookAsync(int id);
    Task<List<Book>> GetBooksPaged(int pageNo, int pageSize);
    Task<Book> AddBookAsync(Book book);
    Task<Book> UpdateBookAsync(Book book);
    void DeleteBookAsync(int id);

}
