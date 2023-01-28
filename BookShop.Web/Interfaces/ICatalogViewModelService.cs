using BookShop.BLL.Entities.Enums;
using BookShop.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BookShop.Web.Interfaces;

public interface ICatalogViewModelService
{
    Task<CatalogViewModel> GetCatalogItems(int pageIndex, int itemsPage, string? searchQuery, int? AuthorId, Cover? cover, Genre? genre, Language? lang);
    Task<IEnumerable<SelectListItem>> GetAuthors();
    IEnumerable<SelectListItem> GetStaticDataFromEnum<T>(Enum value) where T : struct, Enum;

}
