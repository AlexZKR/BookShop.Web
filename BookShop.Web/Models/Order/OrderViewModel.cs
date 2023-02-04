using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShop.Web.Models.Order;
public class OrderViewModel
{
    public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();

    public DateTimeOffset OrderDate { get; set; }
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
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }

    //address data
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? PostCode { get; set; }

    //select list filters
    public int Region { get; set; }
    public int PaymentType { get; set; }
    public int DeliveryType { get; set; }

}
