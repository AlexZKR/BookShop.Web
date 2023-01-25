using BookShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.DAL.Data;


public class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

    }
}