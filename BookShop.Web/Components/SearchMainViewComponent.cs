using BookShop.BLL;
using BookShop.Web.Extensions;
using BookShop.Web.Interfaces;
using BookShop.Web.Models.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Components;

public class SearchMainViewComponent : ViewComponent
{
    private readonly ICatalogViewModelService catalogViewModelService;

    public SearchMainViewComponent(ICatalogViewModelService catalogViewModelService)
    {
        this.catalogViewModelService = catalogViewModelService;
    }

    public IViewComponentResult Invoke(string? searchString)
    {

        catalogViewModelService.GetCatalogViewModel(searchQuery: searchString, username: HttpContext.GetUsername());
        return View(new CatalogViewModel());
    }
}