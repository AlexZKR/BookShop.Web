using System.ComponentModel.DataAnnotations;

namespace BookShop.Web.Models.Catalog;

public class ProductDetailsViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? AuthorName { get; set; }
    public string? PictureUrl { get; set; }
    public double Price { get; set; }
    public double DiscountedPrice { get; set; }
    public double Discount { get; set; }
    public bool IsOnDiscount { get; set; }
    public bool IsFavourite { get; set; }

    //properties table

    [Display(Name = "В наличии")]
    public bool IsAvailable { get; set; }
    [Display(Name = "Количество страниц")]
    public int PagesCount { get; set; }
    [Display(Name = "Жанр")]
    public string? Genre { get; set; }
    [Display(Name = "Язык")]
    public string? Language { get; set; }
    [Display(Name = "Обложка")]
    public string? Cover { get; set; }

    //nav data
    public int AuthorId { get; set; }
    public string? Username { get; set; }
    public int Quantity { get; set; }

}