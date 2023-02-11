using Ardalis.Specification;
using BookShop.BLL.Entities.Products;
using BookShop.BLL.Interfaces;

namespace BookShop.BLL.Specifications.CatalogSpecifications;

public class FavouritesForUserSpecification<T> : Specification<T> where T : BaseProduct, IAggregateRoot
{
    public FavouritesForUserSpecification(string[] Ids)
    {
        Query.Where(x => Ids.Contains(x.Id.ToString()));
    }
}