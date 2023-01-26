using BookShop.DAL.Data;
using BookShop.DAL.Entities;

namespace BookShop.Web.Services.Intefaces;

public interface IFavouriteService
{
    /// <summary>
    /// If item in favs - removes it. If not - adds it.
    /// </summary>
    /// <param name="user">User favs to add to</param>
    /// <param name="Id">Id of item to add</param>
    /// <returns>Bool indicating whether item was removed or added to favs</returns>
    Task<bool> UpdateFavourite(ApplicationUser user, string Id);

    /// <summary>
    /// Removes all favs from the user
    /// </summary>
    /// <param name="user"></param>
    /// <returns>Success or not</returns>
    Task<bool> RemoveAllFavourites(ApplicationUser user);
    /// <summary>
    /// Checks list of books if they are in favs of the user provided
    /// </summary>
    /// <param name="user"></param>
    /// <param name="booksToCheck">List of books to check for favs</param>
    /// <returns>List of books with checked fav properties</returns>
    public List<Book> CheckFavourites(ApplicationUser user, List<Book> booksToCheck);
}