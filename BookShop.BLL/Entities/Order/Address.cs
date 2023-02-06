#pragma warning disable CS8618 // Required by Entity Framework


using System.ComponentModel.DataAnnotations;
using BookShop.BLL.Entities.Enums;


namespace BookShop.BLL.Entities.Order;

public class Address //Value object
{
    public Address()
    {
    }

    public Address(string? street,
                   string? city,
                   Region region,
                   string? postCode)
    {
        Street = street;
        City = city;
        Region = region;
        PostCode = postCode;
    }

    public string? Street { get; set; }
    public string? City { get; set; }
    [Display(Name = "Область")]
    public Region Region { get; set; }
    public string? PostCode { get; set; }
}