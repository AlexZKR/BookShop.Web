using BookShop.BLL.Entities.Products;
using BookShop.BLL.Exceptions;
using BookShop.BLL.Interfaces;
using BookShop.BLL.Specifications.CatalogSpecifications;

namespace BookShop.BLL.Services;

public class BookCatalogService : IBookCatalogService
{
    private readonly IAppLogger<BookCatalogService> logger;
    private readonly IRepository<Book> bookRepository;
    private readonly IRepository<Author> authorRepository;

    public BookCatalogService(IAppLogger<BookCatalogService> logger,
                              IRepository<Book> bookRepository,
                              IRepository<Author> authorRepository)
    {
        this.logger = logger;
        this.bookRepository = bookRepository;
        this.authorRepository = authorRepository;
    }
    public async Task<List<Book>> GetCatalogItems(string username,
                                                  string? searchQuery,
                                                  int pageIndex = 0,
                                                  int itemsPage = SD.ITEMS_PER_PAGE,
                                                  int? AuthorId = 0,
                                                  int? cover = null,
                                                  int? genre = null,
                                                  int? lang = null)
    {
        logger.LogInformation("GetCatalogItems called");

        List<Book> itemsOnPage;
        if (searchQuery == null)
        {
            var paginatedFilterSpec =
             new BookCatalogFilterPaginatedSpecification(skip: itemsPage * pageIndex, take: itemsPage, AuthorId, cover, genre, lang);

            itemsOnPage = await bookRepository.ListAsync(paginatedFilterSpec);
        }
        else
        {
            var filterSearchQuerySpec =
            new BookCatalogSearchQuerySpecification(searchQuery);
            itemsOnPage = await bookRepository.ListAsync(filterSearchQuerySpec);
        }
        return itemsOnPage;
    }

    public async Task<List<Book>> GetTopSoldItems(int quantity, string username)
    {
        var spec = new BookCatalogGetNumberOfTopSoldItemsSpecification(quantity);
        var products = await bookRepository.ListAsync(spec);
        return products!;
    }

    public async Task<IEnumerable<Author>> GetAuthors()
    {
        logger.LogInformation("GetAuthors called");
        var authors = await authorRepository.ListAsync();
        return authors;
    }



    public async Task<int> TotalItemsCountAsync(string? searchQuery,
                                                int? AuthorId,
                                                int? cover,
                                                int? genre,
                                                int? lang,
                                                int pageIndex = 0,
                                                int itemsPage = SD.ITEMS_PER_PAGE)
    {

        if (searchQuery == null)
        {
            var paginatedFilterSpec = new BookCatalogFilterPaginatedSpecification(skip: itemsPage
                * pageIndex, take: itemsPage, AuthorId, cover, genre, lang);
            return await bookRepository.CountAsync(paginatedFilterSpec);
        }
        else
        {
            var filterSearchQuerySpec = new BookCatalogSearchQuerySpecification(searchQuery);
            return await bookRepository.CountAsync(filterSearchQuerySpec);
        }
        throw new NotFoundException("Error when counting total items");
    }


    public async Task<Book> GetBookAsync(int id)
    {
        var spec = new BookWithAuthorSpecification(id);
        var book = await bookRepository.FirstOrDefaultAsync(spec);
        if(book == null) throw new NotFoundException($"Book with id {id} was not found");
        return book;
    }

    public async Task<Book> AddBookAsync(Book book)
    {
        return book = await bookRepository.AddAsync(book);
    }
    public async Task<Book> UpdateBookAsync(Book book)
    {
        await bookRepository.UpdateAsync(book);
        return book;
    }
    public async void DeleteBookAsync(Book book)
    {
        await bookRepository.DeleteAsync(book);
    }

}