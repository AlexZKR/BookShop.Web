using System.Reflection;
using BookShop.BLL.Entities.Basket;
using BookShop.BLL.Entities.Order;
using BookShop.BLL.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.DAL.Data;

#pragma warning disable CS8618 // Disabling null value warnings
public class AppDbContext : DbContext
{
    //basket tables
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }

    //order tables
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    //product tables
    public DbSet<BaseProduct> BaseProducts { get; set; }
    public DbSet<UserFavourites> Favourites { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // var nav = builder.Entity<Order>().Metadata.FindNavigation(nameof(Order.ShipToAddress));
        // nav?.SetPropertyAccessMode(PropertyAccessMode.Field);
        // nav = builder.Entity<Order>().Metadata.FindNavigation(nameof(Order.Buyer));
        // nav?.SetPropertyAccessMode(PropertyAccessMode.Field);
        // nav = builder.Entity<Order>().Metadata.FindNavigation(nameof(Order.OrderItems));
        // nav?.SetPropertyAccessMode(PropertyAccessMode.Field);
        base.OnModelCreating(builder);
        builder.Entity<Order>().OwnsOne(o => o.OrderInfo);
        builder.Entity<Order>().OwnsOne(o => o.Buyer);
        builder.Entity<Order>().OwnsOne(o => o.Address);


    }
}

