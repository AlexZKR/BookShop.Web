using System.ComponentModel.DataAnnotations;
using BookShop.Admin.Models.Product;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShop.Admin.ViewModels.Catalog
{
    public class ProductViewModel : BaseViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Наименование")]
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(100, ErrorMessage = "До 100 символов")]
        public string? Name { get; set; }
        [Display(Name = "Описание")]
        [MaxLength(1500, ErrorMessage = "До 1500 символов")]
        public string? Description { get; set; }
        [Display(Name = "Количество страниц")]
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(0, int.MaxValue, ErrorMessage = "Не меньше нуля")]
        public int PagesCount { get; set; }
        // [Display(Name = "Жанр")]
        // public Genre Genre { get; set; }
        // [Display(Name = "Обложка")]
        // public Cover Cover { get; set; }
        // [Display(Name = "Язык")]
        // public Language Language { get; set; }
        // [Display(Name = "Тэг")]
        // public Tag Tag { get; set; }
        public string? GenreDisplay { get; set; }
        public string? CoverDisplay { get; set; }
        public string? LanguageDisplay { get; set; }
        public string? TagDisplay { get; set; }
       
        [Display(Name = "Полная цена, руб.")]
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(0, int.MaxValue, ErrorMessage = "Не меньше нуля")]
        public double FullPrice { get; set; }
        [Display(Name = "Скидка, руб.")]
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(0, int.MaxValue, ErrorMessage = "Не меньше нуля")]
        public double Discount { get; set; }
        [Display(Name = "Цена со скидкой, руб.")]
        public double DiscountedPrice => FullPrice - (FullPrice * Discount);
        [Display(Name = "Количество, шт.")]
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(0, double.MaxValue)]
        public int Quantity { get; set; }
        [Display(Name = "Продано, шт.")]
        [Range(0, double.MaxValue)]
        public int Sold { get; set; }
        [Display(Name = "Изображение")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string? PictureUri { get; set; }
        [Display(Name = "Рейтинг")]
        public double Rating { get; set; }
        public int AuthorId { get; set; }

        //filters
    [Display(Name = "Обложка")]
    public int? Cover { get; set; }
    [Display(Name = "Жанр")]
    public int? Genre { get; set; }
    [Display(Name = "Язык")]
    public int? Lang { get; set; }
    [Display(Name = "Автор")]
    public int? Author { get; set; }

    //static data
    [Display(Name = "Жанр")]
    public List<SelectListItem>? Genres { get; set; }
    [Display(Name = "Язык")]
    public List<SelectListItem>? Languages { get; set; }
    [Display(Name = "Обложка")]
    public List<SelectListItem>? Covers { get; set; }
    [Display(Name = "Автор")]
    public List<SelectListItem>? Authors { get; set; }

    }
}