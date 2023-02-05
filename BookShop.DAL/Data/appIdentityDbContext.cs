using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookShop.DAL.Data;

public class appIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public appIdentityDbContext(DbContextOptions<appIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
