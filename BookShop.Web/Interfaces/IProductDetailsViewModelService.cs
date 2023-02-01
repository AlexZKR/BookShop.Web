using BookShop.Web.Models.Catalog;

namespace BookShop.Web.Services;

public interface IProductDetailsViewModelService
{
    Task<ProductDetailsViewModel> CreateViewModel(int id, string username);
}
