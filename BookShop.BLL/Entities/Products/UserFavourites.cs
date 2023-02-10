using System.ComponentModel.DataAnnotations;
using BookShop.BLL.Interfaces;

namespace BookShop.BLL.Entities.Products;

public class UserFavourites : ICatalogAggregateRoot
{
    [Key]
    public string? Username { get; set; }
    public string Favourites { get; set; } = String.Empty;

    public List<string> GetFavs()
    {
        if (Favourites != null)
            return Favourites.Split(',').ToList();
        else
            return new List<string>();
    }
}