using BookShop.BLL.Entities.Products;
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
    public async Task<List<Book>> GetCatalogItems(string username, int pageIndex, int itemsPage, string? searchQuery, int? AuthorId, int? cover, int? genre, int? lang)
    {
        logger.LogInformation("GetCatalogItems called");

        List<Book> itemsOnPage;
        //first configuring filters, then using them to query data from repos
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

}