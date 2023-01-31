using BookShop.Web.Models.Catalog;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BookShop.Web.Interfaces;

public interface ICatalogViewModelService
{
    Task<CatalogViewModel> GetCatalogItems(string username, int pageIndex, int itemsPage, string? searchQuery, int? AuthorId, int? cover, int? genre, int? lang);
    Task<IEnumerable<SelectListItem>> GetAuthors();
    IEnumerable<SelectListItem> GetStaticDataFromEnum<T>(Enum value) where T : struct, Enum;

}
