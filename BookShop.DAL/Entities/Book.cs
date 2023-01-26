using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bestBuild.DAL;
using Microsoft.AspNetCore.Http;

namespace BookShop.DAL.Entities;

public class Book : BaseProduct
{
    [Required]
    [DataType(DataType.Text)]
    [StringLength(30)]
    [Display(Name = "Наименование товара")]
    public string Name { get; set; } = "";

    [DataType(DataType.MultilineText)]
    [StringLength(500)]
    [Display(Name = "Описание товара")]
    public string Description { get; set; } = "";

    [Range(0, int.MaxValue)]
    [Display(Name = "Количество страниц")]
    public int PagesCount { get; set; }



    //enums

    [DataType(DataType.Text)]
    [StringLength(500)]
    [Display(Name = "Жанр")]
    public Genre Genre { get; set; }

    [DataType(DataType.Text)]
    [StringLength(500)]
    [Display(Name = "Обложка")]
    public Cover Cover { get; set; }
    [DataType(DataType.Text)]
    [StringLength(500)]
    [Display(Name = "Язык")]
    public Language Language { get; set; }

    [DataType(DataType.Text)]
    [StringLength(500)]
    [Display(Name = "Тэг")]
    public Tag Tag { get; set; } = Tag.None;



    //$$
    [Display(Name = "Полная стоимость товара, руб.")]
    [Range(0, int.MaxValue)]
    public double Price { get; set; } = 0;
    [Display(Name = "В наличии")]
    public int Quantity { get; set; } = 0;

    [Display(Name = "Продано, шт.")]
    [Range(0, int.MaxValue)]
    public int Sold { get; set; }


    [Range(0, 1)]
    [Display(Name = "Скидка")]
    public double Discount { get; set; } = 0;
    [NotMapped]
    [Display(Name = "Цена со скидкой")]
    public double DiscountedPrice => Price - (Price * Discount);



    [Display(Name = "Изображение")]
    public string ImagePath { get; set; } = SD.NO_PHOTO;
    [NotMapped]
    [Display(Name = "Изображение")]
    public IFormFile ImageFile { get; set; } = null!;


    //nav
    [ForeignKey("AuthorId")]
    public int AuthorId { get; set; }
    [Display(Name = "Автор")]
    public Author Author { get; set; } = null!;


    //ratings

}