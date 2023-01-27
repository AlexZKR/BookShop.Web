using BookShop.BLL.Entities.Products;
using BookShop.BLL.Interfaces;
using BookShop.DAL.Data;
using BookShop.Web.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BookShop.Web.Services;

public class FavouriteService<T> : IFavouriteService<T> where T : BaseProduct, IAggregateRoot
{
    private readonly UserManager<ApplicationUser> userManager;

    public FavouriteService(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
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


    public async Task<List<T>> GetFavouritesForUser(ApplicationUser user, IRepository<T> repository)
    {
        if (user is null) return null!;

        string[] userFavs = user.Favourites.Split(',');

        if (userFavs is null) return null!;

        //var favItems = await repository.Set<T>().Where(x => userFavs.Contains(x.Id.ToString())).ToListAsync();
        var favItems = new List<T>();

        return favItems;
    }
}