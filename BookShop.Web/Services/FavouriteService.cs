using BookShop.BLL.Entities.Products;
using BookShop.BLL.Interfaces;
using BookShop.DAL.Data;
using BookShop.Web.Interfaces;
using BookShop.Web.Specifications;
using Microsoft.AspNetCore.Identity;

namespace BookShop.Web.Services;

public class FavouriteService<T> : IFavouriteService<T> where T : BaseProduct, IAggregateRoot
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IRepository<T> productRepository;



    public FavouriteService(UserManager<ApplicationUser> userManager, IRepository<T> productRepository)
    {
        this.userManager = userManager;
        this.productRepository = productRepository;
    }

    public List<T> CheckFavourites(ApplicationUser user, List<T> entitiesToCheck)
    {
        string[] items = user?.Favourites?.Split(',')!;
        if (items is null)
            return null!;

        foreach (var item in entitiesToCheck)
        {
            if (items.Contains(item.Id.ToString()))
                item.IsFavourite = true;
        }
        return entitiesToCheck;
    }

    public bool CheckIfFavourite(T entity, ApplicationUser user)
    {
        string[] items = user?.Favourites?.Split(',')!;
        if (items.Contains(entity.Id.ToString()))
            return true;
        else
            return false;
    }

    public async Task<bool> RemoveFromFavourites(ApplicationUser user, string Id)
    {
        string postIdComplate = ',' + Id;
        bool isExisted = false;
        user.Favourites = user.Favourites.Replace(postIdComplate, "");
        isExisted = true;
        await userManager.UpdateAsync(user);
        return isExisted;
    }

    public async Task<bool> UpdateFavourite(ApplicationUser user, string Id)
    {
        string postIdComplate = ',' + Id;
        bool isExisted = false;

        if (user.Favourites.IndexOf(Id) > -1)
        {
            user.Favourites = user.Favourites.Replace(postIdComplate, "");
            isExisted = true;
        }
        else
        {
            user.Favourites += postIdComplate;
            isExisted = false;
        }

        await userManager.UpdateAsync(user);
        return isExisted;
    }


    public async Task<List<T>> GetFavouritesForUser(ApplicationUser user)
    {
        if (user is null) return null!;

        string[] userFavs = user.Favourites.Split(',');

        if (userFavs is null) return null!;

        var spec = new FavouritesForUserSpecification<T>(user, userFavs);
        var favItems = await productRepository.ListAsync(spec);

        return favItems;
    }
}