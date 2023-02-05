using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookShop.BLL.Entities.Enums;

namespace BookShop.BLL.Entities.Products;

public class Book : BaseProduct
{

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

    //nav
    [ForeignKey("AuthorId")]
    public int AuthorId { get; set; }
    [Display(Name = "Автор")]
    public Author Author { get; set; } = null!;


}