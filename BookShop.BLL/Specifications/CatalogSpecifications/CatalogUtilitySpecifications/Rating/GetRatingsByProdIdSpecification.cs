using Ardalis.Specification;
using BookShop.BLL.Entities.Products;
using BookShop.BLL.Interfaces;

namespace BookShop.BLL.Specifications.CatalogSpecifications;

public class GetRatingsByProdIdSpecification : Specification<ProductRating>
{

    public GetRatingsByProdIdSpecification(int productId)
    {
        Query.Where(x => x.ProductId == productId);
    }
}