using Ardalis.Specification;
using BookShop.BLL.Entities.Products;
using BookShop.BLL.Interfaces;

namespace BookShop.BLL.Specifications.CatalogSpecifications;

public class GetRatingByUsernameAndProdIdSpecification  : Specification<ProductRating>
{
    public GetRatingByUsernameAndProdIdSpecification (string username, int productId)
    {
        Query.Where(x =>
        (x.Username == username) && (x.ProductId == productId));
    }
}
