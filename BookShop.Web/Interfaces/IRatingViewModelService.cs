using BookShop.Web.Models.Catalog;

namespace BookShop.Web.Services;

public interface IRatingViewModelService
{
   RatingViewModel CreateViewModel(int rating);
}
