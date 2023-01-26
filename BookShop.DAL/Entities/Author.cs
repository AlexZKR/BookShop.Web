using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using bestBuild.DAL;
using Microsoft.AspNetCore.Http;

namespace BookShop.DAL.Entities
{
    public class Author : BaseProduct
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(30)]
        [Display(Name = "Имя")]
        public string? Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(30)]
        [Display(Name = "Описание")]
        public string? Description { get; set; }
        [Display(Name = "Изображение")]
        public string ImagePath { get; set; } = SD.NO_PHOTO;
        [NotMapped]
        [Display(Name = "Изображение")]
        public IFormFile ImageFile { get; set; } = null!;


        //nav
        public List<Book> Books { get; set; } = null!;
    }
}