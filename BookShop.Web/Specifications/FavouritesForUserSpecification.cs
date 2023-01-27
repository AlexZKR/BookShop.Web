using Ardalis.Specification;
using BookShop.BLL.Entities.Products;
using BookShop.BLL.Interfaces;
using BookShop.DAL.Data;

namespace BookShop.Web.Specifications;

public class FavouritesForUserSpecification<T> : Specification<T> where T : BaseProduct, IAggregateRoot
{
    public FavouritesForUserSpecification(ApplicationUser user, string[] items)
    {
        //var favItems = await repository.Set<T>().Where(x => userFavs.Contains(x.Id.ToString())).ToListAsync();
        Query.Where(x => items.Contains(x.Id.ToString()));
    }
}