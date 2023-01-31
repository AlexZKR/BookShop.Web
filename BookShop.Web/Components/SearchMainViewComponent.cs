using BookShop.Web.Models.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Components;

public class SearchMainViewComponent : ViewComponent
{

    public SearchMainViewComponent()
    {
    }

    public IViewComponentResult Invoke()
    {
        return View(new CatalogViewModel());
    }
}