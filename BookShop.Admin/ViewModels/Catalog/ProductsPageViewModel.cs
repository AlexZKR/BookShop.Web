namespace BookShop.Admin.ViewModels.Catalog;

public class ProductsPageViewModel : BaseViewModel
{

    public List<ProductViewModel> Products { get; set; } = new();
    public PaginationViewModel Pagination { get; set; } = new();

}