//#pragma warning disable CS8618 // Required by Entity Framework

using System.ComponentModel.DataAnnotations.Schema;
using BookShop.BLL.Entities.Enums;

namespace BookShop.DAL.Entities.Order
{
    public class Address // ValueObject
    {
        public string? Street { get; private set; }
        public string? City { get; private set; }
        public string? Region { get; private set; }
        public string? PostCode { get; private set; }

        // public Address() { }

        // public Address(string street, string city, string region, string zipcode)
        // {
        //     Street = street;
        //     City = city;
        //     Region = region;
        //     PostCode = zipcode;
        // }
    }
}