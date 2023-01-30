using BookShop.BLL.Entities.Products;

namespace BookShop.BLL.Interfaces;

public interface IFavouriteService<T> where T : BaseProduct, IAggregateRoot
{
    // public Task<bool> CheckIfFavourite(string username, T entity);
    public bool CheckIfFavourite(string username, T entity);
    Task<bool> RemoveFromFavourites(string username, string Id);
    public Task<List<T>> GetFavouritesForUser(string username);
    public Task<bool> UpdateFavourite(string username, string Id);
}