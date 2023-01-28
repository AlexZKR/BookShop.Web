using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShop.Web.Models;

public class CatalogViewModel
{
    public List<CatalogItemViewModel>? CatalogItems { get; set; }

    //filters
    public int? CoversFilter { get; set; }
    public int? GenresFilter { get; set; }
    public int? LanguagesFilter { get; set; }
    public int? AuthorsFilter { get; set; }

    public PaginationViewModel? PaginationInfo { get; set; }
    public string? SearchQuery { get; set; }

    //static data
    public List<SelectListItem>? Genres { get; set; }
    public List<SelectListItem>? Languages { get; set; }
    public List<SelectListItem>? Covers { get; set; }
    public List<SelectListItem>? Authors { get; set; }
}