
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookShop.BLL.Interfaces;

namespace BookShop.BLL.Entities.Products;
public class ProductRating : IAggregateRoot
{

    [Key]
    public string? Username { get; set; }
    [ForeignKey("ProductId")]
    public int ProductId { get; set; }
    public BaseProduct? Product { get; set; }
    [Range(0,5)]
    public int Rating { get; set; }
}