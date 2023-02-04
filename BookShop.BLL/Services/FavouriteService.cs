using BookShop.BLL.Entities.Products;
using BookShop.BLL.Interfaces;
using BookShop.BLL.Specifications.CatalogSpecifications;

namespace BookShop.BLL.Services;

public class FavouriteService<T> : IFavouriteService<T> where T : BaseProduct, IAggregateRoot
{
    private readonly IRepository<T> productRepository;
    private readonly IRepository<UserFavourites> favouritesRepository;

    public FavouriteService(IRepository<T> productRepository,
     IRepository<UserFavourites> favouritesRepository)
    {
        this.productRepository = productRepository;
        this.favouritesRepository = favouritesRepository;
    }

    public bool CheckIfFavourite(string username, T entity)
    {
        //if username == null than user is not authenticated
        if (username == null) return false;
        var favs = GetFavouritesObject(username).Result;
        return favs.Favourites.Split(',').Contains(entity.Id.ToString());
    }

    public async Task<bool> RemoveFromFavourites(string username, string Id)
    {
        string postIdComplate = ',' + Id;
        bool isExisted = false;
        var favs = await GetFavouritesObject(username);

        favs.Favourites = favs.Favourites.Replace(postIdComplate, "");
        isExisted = true;
        await favouritesRepository.UpdateAsync(favs);
        return isExisted;
    }

    public async Task<bool> UpdateFavourite(string username, string Id)
    {
        string postIdComplate = ',' + Id;
        bool isExisted = false;

        var favs = await GetFavouritesObject(username);

        if (favs.Favourites.IndexOf(Id) > -1)
        {
            favs.Favourites = favs.Favourites.Replace(postIdComplate, "");
            isExisted = true;
        }
        else
        {
            favs.Favourites += postIdComplate;
            isExisted = false;
        }

        await favouritesRepository.UpdateAsync(favs);
        return isExisted;
    }


    public async Task<List<T>> GetFavouritesForUser(string username)
    {
        if (username is null) return null!;

        var favs = await GetFavouritesObject(username);

        var spec = new FavouritesForUserSpecification<T>(favs.Favourites.Split(','));
        var favItems = await productRepository.ListAsync(spec);

        return favItems;
    }

    //private helpers

    private async Task<UserFavourites> GetFavouritesObject(string username)
    {
        var spec = new GetFavouritesSpecification(username);
        var favs = await favouritesRepository.FirstOrDefaultAsync(spec);
        if (favs == null)
        {
            favs = new UserFavourites() { Username = username, Favourites = "0" };
            await favouritesRepository.AddAsync(favs);
        }
        return favs;

    }
}