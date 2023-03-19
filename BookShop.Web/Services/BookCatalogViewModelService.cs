using AutoMapper;
using BookShop.BLL;
using BookShop.BLL.Entities.Enums;
using BookShop.BLL.Entities.Products;
using BookShop.BLL.Interfaces;
using BookShop.Web.Infrastructure;
using BookShop.Web.Interfaces;
using BookShop.Web.Models.Catalog;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShop.Web.Services;

public class BookCatalogViewModelService : ICatalogViewModelService
{
    private readonly ILogger<BookCatalogViewModelService> logger;
    private readonly IBookCatalogService bookCatalogService;
    private readonly IFavouriteService<Book> favouriteService;
    private readonly IRatingService ratingService;
    private readonly IMapper mapper;

    public BookCatalogViewModelService(ILoggerFactory loggerFactory,
    IBookCatalogService bookCatalogService,
    IFavouriteService<Book> favouriteService,
    IRatingService ratingService,
    IMapper mapper)
    {
        logger = loggerFactory.CreateLogger<BookCatalogViewModelService>();
        this.bookCatalogService = bookCatalogService;
        this.favouriteService = favouriteService;
        this.ratingService = ratingService;
        this.mapper = mapper;
    }

    public async Task<CatalogViewModel> GetCatalogViewModel(string username, string? searchQuery, int pageIndex = 0, int itemsPage = SD.ITEMS_PER_PAGE, int AuthorId = 0, int? cover = null, int? genre = null, int? lang = null)
    {
        logger.LogInformation("GetCatalogItemsViewModels called");

        List<Book> itemsOnPage = await bookCatalogService.GetCatalogItems(username, searchQuery, pageIndex, itemsPage, AuthorId, cover, genre, lang);

        var vm = new CatalogViewModel()
        {
            CatalogItems = itemsOnPage.Select(b => new CatalogItemViewModel()
            {
                Id = b.Id,
                Name = b.Name,
                AuthorName = b.Author.Name,
                PictureUri = b.PictureUri,
                Price = b.FullPrice,
                DiscountedPrice = b.DiscountedPrice,
                IsOnDiscount = b.Discount != 0,
                IsAvailable = b.Quantity != 0,
                IsFavourite = favouriteService.CheckIfFavourite(username, b),
                Rating = new RatingViewModel { Rating = ratingService.GetRating(username, b.Id).Result },
            }).ToList(),
            FilterInfo = new CatalogFilterViewModel()
            {
                SearchQuery = searchQuery,
                Genres = EnumHelper<Genre>.GetStaticDataFromEnum(Genre.ChildrenLiterature).ToList(),
                Languages = EnumHelper<Language>.GetStaticDataFromEnum(Language.Russian).ToList(),
                Covers = EnumHelper<Cover>.GetStaticDataFromEnum(Cover.HardCover).ToList(),
                Authors = (await GetAuthorsSelectList()).ToList(),
            },
            PaginationInfo = new PaginationViewModel()
            {
                ActualPage = pageIndex,
                ItemsOnPage = itemsOnPage.Count,
                TotalItems = await bookCatalogService.TotalItemsCountAsync(searchQuery, AuthorId, cover, genre, lang, pageIndex, itemsPage),
            }
        };

        vm.PaginationInfo.IsNextPageHasItems = (vm.PaginationInfo.TotalItems - SD.ITEMS_PER_PAGE * vm.PaginationInfo.ActualPage) > 0 ? true : false;
        vm.PaginationInfo.TotalPages = CalculateTotalPages(vm.PaginationInfo.TotalItems, SD.ITEMS_PER_PAGE);
        vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
        vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

        vm.PaginationInfo.IsNextPageHasItems = vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1 ? false : true;

        return vm;
    }

    public async Task<CatalogViewModel> GetTopSoldItemsViewModel(int quantity, string username)
    {
        var products = await bookCatalogService.GetTopSoldItems(quantity, username);

        var vm = new CatalogViewModel()
        {
            CatalogItems = products.Select(p => new CatalogItemViewModel
            {
                Id = p.Id,
                Name = p.Name,
                PictureUri = p.PictureUri,
                Price = p.FullPrice,
                DiscountedPrice = p.DiscountedPrice,
                IsFavourite = favouriteService.CheckIfFavourite(username, p),
                IsOnDiscount = p.Discount > 0 ? true : false,

            }).ToList(),
        };
        return vm;
    }
    public async Task<CatalogItemViewModel> GetCatalogItemViewModelAsync(int prodId)
    {
        var book = await bookCatalogService.GetBookAsync(prodId);
        var vm = mapper.Map<CatalogItemViewModel>(book);
        return vm;
    }


    public async Task<IEnumerable<SelectListItem>> GetAuthorsSelectList()
    {
        logger.LogInformation("GetAuthors called");
        var authorNames = await bookCatalogService.GetAuthors();

        var items = authorNames.Select(n => new SelectListItem() { Text = n.Name, Value = n.Id.ToString() })
            .OrderBy(n => n.Text)
            .ToList();

        var allItem = new SelectListItem() { Value = "0", Text = "Все", Selected = true };
        items.Insert(0, allItem);
        return items;
    }

    private int CalculateTotalPages(int totalItems, int ItemsPerPage)
    {
        int res = 0;
        res = (int)Math.Ceiling((double)totalItems / (double)ItemsPerPage);
        logger.LogInformation($"Total pages: {res}");
        return res;
    }
}