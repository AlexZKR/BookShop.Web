using System.ComponentModel.DataAnnotations;
using BookShop.Admin.Models.Product;

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
        [Display(Name = "Жанр")]
        public Genre Genre { get; set; }
        [Display(Name = "Обложка")]
        public Cover Cover { get; set; }
        [Display(Name = "Язык")]
        public Language Language { get; set; }
        [Display(Name = "Тэг")]
        public Tag Tag { get; set; }
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

    }
}