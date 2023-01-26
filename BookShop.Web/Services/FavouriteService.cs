using BookShop.DAL.Data;
using BookShop.DAL.Entities;
using BookShop.Web.Services.Intefaces;
using Microsoft.AspNetCore.Identity;

namespace BookShop.Web.Services;

public class FavouriteService : IFavouriteService
{
    private readonly UserManager<ApplicationUser> userManager;

    public FavouriteService(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    public List<Book> CheckFavourites(ApplicationUser user, List<Book> booksToCheck)
    {
        string[] items = user?.Favourites?.Split(',')!;
        if (items is null)
            return null!;

        foreach (var item in booksToCheck)
        {
            if (items.Contains(item.ProductId.ToString()))
                item.IsFavourite = true;
        }
        return booksToCheck;
    }

    public Task<bool> RemoveAllFavourites(ApplicationUser user)
    {
        throw new NotImplementedException();
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
}
