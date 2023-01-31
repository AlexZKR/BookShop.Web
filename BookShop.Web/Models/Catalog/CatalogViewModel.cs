
namespace BookShop.Web.Models.Catalog;

public class CatalogViewModel
{
    public List<CatalogItemViewModel>? CatalogItems { get; set; }

    public CatalogFilterViewModel? FilterInfo { get; set; }

    public PaginationViewModel? PaginationInfo { get; set; }



}