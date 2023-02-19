using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookShop.BLL.Interfaces;

namespace BookShop.BLL.Entities.Products
{
    public class Author : BaseEntity, IAggregateRoot
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Имя")]
        public string? Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Описание")]
        public string? Description { get; set; }
        [Display(Name = "Изображение")]
        public string PictureUri { get; set; } = SD.NO_PHOTO;
        [NotMapped]
        [Display(Name = "Изображение")]

        //nav
        public List<Book> Books { get; set; } = new List<Book>();

    }
}