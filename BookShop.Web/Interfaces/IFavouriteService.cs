using BookShop.BLL.Entities.Products;
using BookShop.BLL.Interfaces;
using BookShop.DAL.Data;

namespace BookShop.Web.Interfaces;

public interface IFavouriteService<T> where T : BaseProduct, IAggregateRoot
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
    Task<bool> RemoveFromFavourites(ApplicationUser user, string id);
    /// <summary>
    /// Checks list of books if they are in favs of the user provided
    /// </summary>
    /// <param name="user"></param>
    /// <param name="entitiesToCheck">List of books to check for favs</param>
    /// <returns>List of books with checked fav properties</returns>
    public List<T> CheckFavourites(ApplicationUser user, List<T> entitiesToCheck);

    /// <summary>
    /// Get list of favourite items for specific user
    /// </summary>
    /// <param name="user"></param>
    /// <param name="context"></param>
    /// <returns>List of fav items</returns>
    public Task<List<T>> GetFavouritesForUser(ApplicationUser user, IRepository<T> repo);
}