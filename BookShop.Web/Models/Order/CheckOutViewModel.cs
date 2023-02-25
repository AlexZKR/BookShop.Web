using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShop.Web.Models.Order;
public class CheckOutViewModel
{
    public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();

    public double TotalPrice { get; set; }
    public double FullPrice { get; set; }
    public double DiscountSize { get; set; }
    public int TotalItems { get; set; }
    public bool IsInProccess { get; set; } = true;
    public string? OrderComment { get; set; }

    //static data
    [Display(Name = "Оплата")]
    public List<SelectListItem>? PaymentTypes { get; set; }
    [Display(Name = "Доставка")]
    public List<SelectListItem>? DeliveryTypes { get; set; }
    [Display(Name = "Область")]
    public List<SelectListItem>? Regions { get; set; }


    //client data
    public string? BuyerId { get; set; }

    [Required (ErrorMessage = "Обязательное поле")]
    [MinLength (3, ErrorMessage = "Мин. длина 3 символа")]
    [MaxLength (30, ErrorMessage = "Макс. длина 30 символов")]
    public string? FirstName { get; set; }
    [Required (ErrorMessage = "Обязательное поле")]
    [MinLength (3, ErrorMessage = "Мин. длина 3 символа")]
    [MaxLength (30, ErrorMessage = "Макс. длина 30 символов")]
    public string? LastName { get; set; }
    [Required (ErrorMessage = "Обязательное поле")]
    [RegularExpression(@"^(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})$", ErrorMessage = "Некорректный номер")]
    public string? PhoneNumber { get; set; }
    [Required (ErrorMessage = "Обязательное поле")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Некорректный адрес эл. почты")]
    public string? Email { get; set; }

    //address data
    [Required (ErrorMessage = "Обязательное поле")]
    [MaxLength (100, ErrorMessage = "Макс. длина 100 символов")]
    public string? Street { get; set; }
    [Required (ErrorMessage = "Обязательное поле")]
    [MinLength (3, ErrorMessage = "Мин. длина 3 символа")]
    [MaxLength (30, ErrorMessage = "Макс. длина 50 символов")]

    public string? City { get; set; }
    [Required (ErrorMessage = "Обязательное поле")]
    [RegularExpression(@"\d\d\d\d\d\d", ErrorMessage = "Некорректный почтовый индекс")]
    public string? PostCode { get; set; }

    //select list filters
    public int Region { get; set; }
    public int PaymentType { get; set; }
    public int DeliveryType { get; set; }

}
