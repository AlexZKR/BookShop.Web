
namespace BookShop.Web.Models.Catalog;

public class CatalogViewModel
{
    public List<CatalogItemViewModel> CatalogItems { get; set; } = new List<CatalogItemViewModel>();

    public CatalogFilterViewModel FilterInfo { get; set; } = new CatalogFilterViewModel();

    public PaginationViewModel PaginationInfo { get; set; } = new PaginationViewModel();



}