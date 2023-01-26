using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.DAL.Entities;

public abstract class BaseProduct
{
    [Key]
    public virtual int Id { get; protected set; }
    [NotMapped]
    public bool IsFavourite { get; set; }
}