using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.DAL.Entities.Products;

public abstract class BaseProduct : BaseEntity
{
    [NotMapped]
    public bool IsFavourite { get; set; }
}