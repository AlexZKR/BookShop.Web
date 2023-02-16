using BookShop.BLL.Entities.Enums;
using BookShop.BLL.Entities.Products;
using BookShop.BLL.Interfaces;
using BookShop.BLL.Specifications.CatalogSpecifications;
using BookShop.Web.Infrastructure;
using BookShop.Web.Models.Catalog;

namespace BookShop.Web.Services;

public class ProductDetailsViewModelService : IProductDetailsViewModelService
{
    private readonly ILogger<BookCatalogViewModelService> logger;
    private readonly IRepository<Book> bookRepository;
    private readonly IImageService uriComposer;
    private readonly IFavouriteService<Book> favouriteService;

    public ProductDetailsViewModelService(ILoggerFactory loggerFactory,
    IRepository<Book> bookRepository,
    IImageService uriComposer,
    IFavouriteService<Book> favouriteService)
    {
        logger = loggerFactory.CreateLogger<BookCatalogViewModelService>();
        this.bookRepository = bookRepository;
        this.uriComposer = uriComposer;
        this.favouriteService = favouriteService;
    }

    public async Task<ProductDetailsViewModel> CreateViewModel(int id, string username)
    {
        var spec = new BookWithAuthorSpecification(id);
        var item = await bookRepository.FirstOrDefaultAsync(spec);
        var vm = new ProductDetailsViewModel()
        {
            Id = id,
            Name = item!.Name,
            Description = item.Description,
            PagesCount = item.PagesCount,
            AuthorName = item.Author.Name,
            PictureUrl = item.ImagePath,
            Price = item.FullPrice,
            DiscountedPrice = item.DiscountedPrice,
            Discount = item.Discount,
            IsAvailable = false,
            IsOnDiscount = false,
            IsFavourite = favouriteService.CheckIfFavourite(username, item),

            Genre = EnumHelper<Genre>.GetDisplayValue(Genre.Fiction),
            Language = EnumHelper<Language>.GetDisplayValue(Language.Russian),
            Cover = EnumHelper<Cover>.GetDisplayValue(Cover.HardCover),

            //nav data
            AuthorId = item.Author.Id,
            Username = username
        };
        if (item.Quantity > 0) vm.IsAvailable = true;
        if (item.Discount > 0) vm.IsOnDiscount = true;
        return vm;
    }


}
