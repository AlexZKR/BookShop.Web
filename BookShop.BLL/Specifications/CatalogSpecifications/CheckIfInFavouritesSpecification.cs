using Ardalis.Specification;
using BookShop.BLL.Entities.Products;

namespace BookShop.BLL.Specifications.CatalogSpecifications;

public class GetFavouritesSpecification : Specification<UserFavourites>
{
    public GetFavouritesSpecification(string username)
    {
        Query.Where(f => f.Username == username);
    }
}
