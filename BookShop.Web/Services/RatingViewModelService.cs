using BookShop.Web.Models.Catalog;

namespace BookShop.Web.Services;

public class RatingViewModelService : IRatingViewModelService
{
    public RatingViewModel CreateViewModel(int rating)
    {
        return new RatingViewModel
        {
            Rating = rating,
        };
    }
}