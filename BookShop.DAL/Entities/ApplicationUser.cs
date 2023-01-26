using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BookShop.DAL.Entities;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [ForeignKey("ProductId")]
    // public int ProductId { get; set; }
    public List<Book> Favourite { get; set; } = new List<Book>();
}

