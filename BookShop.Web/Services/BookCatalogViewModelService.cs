using BookShop.BLL.Entities.Enums;
using BookShop.BLL.Entities.Products;
using BookShop.BLL.Interfaces;
using BookShop.BLL.Specifications.CatalogSpecifications;
using BookShop.Web.Infrastructure;
using BookShop.Web.Interfaces;
using BookShop.Web.Models.Catalog;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShop.Web.Services;

public class BookCatalogViewModelService : ICatalogViewModelService
{
    private readonly ILogger<BookCatalogViewModelService> logger;
    private readonly IRepository<Book> bookRepository;
    private readonly IRepository<Author> authorRepository;
    private readonly IUriComposer uriComposer;
    private readonly IFavouriteService<Book> favouriteService;

    public BookCatalogViewModelService(ILoggerFactory loggerFactory,
    IRepository<Book> bookRepository,
    IRepository<Author> authorRepository,
    IUriComposer uriComposer,
    IFavouriteService<Book> favouriteService)
    {
        logger = loggerFactory.CreateLogger<BookCatalogViewModelService>();
        this.bookRepository = bookRepository;
        this.authorRepository = authorRepository;
        this.uriComposer = uriComposer;
        this.favouriteService = favouriteService;
    }

    //todo: split method cause it violates single responsibility principle, add sorting and tag filter on top of the catalog
    public async Task<CatalogViewModel> GetCatalogItems(string username, int pageIndex, int itemsPage, string? searchQuery, int? AuthorId, int? cover, int? genre, int? lang)
    {
        logger.LogInformation("GetCatalogItems called");

        List<Book> itemsOnPage;
        int totalItemsCount;

        //first configuring filters, then using them to query data from repos
        if (searchQuery == null)
        {
            var filterSpec =
             new BookCatalogFilterSpecification(AuthorId, cover, genre, lang);
            var paginatedFilterSpec =
             new BookCatalogFilterPaginatedSpecification(skip: itemsPage * pageIndex, take: itemsPage, AuthorId, cover, genre, lang);

            itemsOnPage = await bookRepository.ListAsync(paginatedFilterSpec);
            totalItemsCount = await bookRepository.CountAsync(filterSpec);
        }
        else
        {
            var filterSearchQuerySpec =
            new BookCatalogSearchQuerySpecification(searchQuery);

            itemsOnPage = await bookRepository.ListAsync(filterSearchQuerySpec);
            totalItemsCount = await bookRepository.CountAsync(filterSearchQuerySpec);
        }


        var vm = new CatalogViewModel()
        {
            CatalogItems = itemsOnPage.Select(b => new CatalogItemViewModel()
            {
                Id = b.Id,
                Name = b.Name,
                AuthorName = b.Author.Name,
                PictureUri = uriComposer.ComposePicUri(b.ImagePath),
                Price = b.FullPrice,
                DiscountedPrice = b.DiscountedPrice,
                IsOnDiscount = b.Discount != 0,
                IsAvailable = b.Quantity != 0,
                IsFavourite = favouriteService.CheckIfFavourite(username, b),
            }).ToList(),
            FilterInfo = new CatalogFilterViewModel()
            {
                SearchQuery = searchQuery,
                Genres = EnumHelper<Genre>.GetStaticDataFromEnum(Genre.ChildrenLiterature).ToList(),
                Languages = EnumHelper<Language>.GetStaticDataFromEnum(Language.Russian).ToList(),
                Covers = EnumHelper<Cover>.GetStaticDataFromEnum(Cover.HardCover).ToList(),
                Authors = (await GetAuthors()).ToList(),
            },
            PaginationInfo = new PaginationViewModel()
            {
                ActualPage = pageIndex,
                ItemsPerPage = itemsOnPage.Count,
                TotalItems = totalItemsCount,
                TotalPages = int.Parse(Math.Ceiling(((decimal)totalItemsCount / itemsPage)).ToString())
            }
        };

        vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
        vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";
        return vm;
    }

    public async Task<CatalogViewModel> GetTopSoldItems(int quantity, string username)
    {
        var spec = new BookCatalogGetNumberOfTopSoldItemsSpecification(quantity);
        var products = await bookRepository.ListAsync(spec);

        var vm = new CatalogViewModel()
        {
            CatalogItems = products.Select( p => new CatalogItemViewModel
            {
                 Id = p.Id,
                 Name = p.Name,
                 PictureUri = uriComposer.ComposePicUri(p.ImagePath),
                 Price = p.FullPrice,
                 DiscountedPrice = p.DiscountedPrice,
                 IsFavourite = favouriteService.CheckIfFavourite(username, p),
                 IsOnDiscount = p.Discount > 0 ? true : false,

            }).ToList(),
        };
        return vm;
    }


    public async Task<IEnumerable<SelectListItem>> GetAuthors()
    {
        logger.LogInformation("GetAuthors called");
        var authorNames = await authorRepository.ListAsync();

        var items = authorNames.Select(n => new SelectListItem() { Text = n.Name, Value = n.Id.ToString() })
            .OrderBy(n => n.Text)
            .ToList();

        var allItem = new SelectListItem() { Value = "0", Text = "Все", Selected = true };
        items.Insert(0, allItem);
        return items;
    }

}