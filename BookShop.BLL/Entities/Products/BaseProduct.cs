using System.ComponentModel.DataAnnotations.Schema;
using BookShop.BLL.Interfaces;

namespace BookShop.BLL.Entities.Products;

public abstract class BaseProduct : BaseEntity, IAggregateRoot
{
    [NotMapped]
    public bool IsFavourite { get; set; }
}