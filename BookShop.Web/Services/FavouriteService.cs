using BookShop.DAL.Data;
using BookShop.DAL.Entities;
using BookShop.DAL.Entities.Products;
using BookShop.Web.Services.Intefaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Web.Services;

public class FavouriteService<T> : IFavouriteService<T> where T : BaseProduct
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


    public async Task<List<T>> GetFavouritesForUser(ApplicationUser user, AppDbContext context)
    {
        if (user is null) return null!;

        string[] userFavs = user.Favourites.Split(',');

        if (userFavs is null) return null!;

        var favItems = await context.Set<T>().Where(x => userFavs.Contains(x.Id.ToString())).ToListAsync();

        return favItems;
    }
}