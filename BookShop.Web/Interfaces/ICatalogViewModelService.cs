using BookShop.BLL;
using BookShop.Web.Models.Catalog;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BookShop.Web.Interfaces;

public interface ICatalogViewModelService
{
    Task<CatalogViewModel> GetCatalogViewModel(string username,string? searchQuery, int pageIndex = 0, int itemsPage = SD.ITEMS_PER_PAGE,  int AuthorId = 0, int? cover = null, int? genre = null, int? lang = null);
    Task<IEnumerable<SelectListItem>> GetAuthorsSelectList();
    Task<CatalogViewModel> GetTopSoldItemsViewModel(int quantity, string username);
}
