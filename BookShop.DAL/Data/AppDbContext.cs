using BookShop.BLL.Entities.Basket;
using BookShop.BLL.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace BookShop.DAL.Data;

#pragma warning disable CS8618 // Disabling null value warnings
public class AppDbContext : DbContext
{
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
    }
}