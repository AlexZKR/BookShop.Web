using Microsoft.AspNetCore.Identity;

namespace BookShop.DAL.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string Favourites { get; set; } = "0";
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

