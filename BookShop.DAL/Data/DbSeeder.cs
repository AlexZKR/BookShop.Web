
using BookShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookShop.DAL.Data;

public class DbSeeder
{
    public static async Task SeedDataAsync(AppDbContext context, ILogger logger, int retry = 0)
    {
        var retryForAvailability = retry;
        try
        {
            context.Database.EnsureCreated();

            if (!await context.Authors.AnyAsync())
            {
                await context.Authors.AddRangeAsync(
                    GetPreconfiguredAuthors());

                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            if (retryForAvailability >= 10) throw;

            retryForAvailability++;

            logger.LogError(ex.Message);
            await SeedDataAsync(context, logger, retryForAvailability);
            throw;
        }
    }

    private static IEnumerable<Author> GetPreconfiguredAuthors()
    {
        return new List<Author>()
        {
            new Author
            {
                Name = "Олдос Хаксли",
                Description = "test desc",
                Books = new List<Book>
                {
                    new Book
                    {
                        Name = "О дивный новый мир",
                        Description = "test desc",
                        PagesCount = 320,
                        Genre = Genre.Fiction,
                        Language = Language.Russian,
                        Cover = Cover.HardCover,

                        Price = 12,
                        Discount = 0.10,
                        Quantity = 10,

                    },
                    new Book
                    {
                        Name = "Остров",
                        Description = "test desc",
                        PagesCount = 250,
                        Genre = Genre.Fiction,
                        Language = Language.Russian,
                        Cover = Cover.HardCover,

                        Price = 8,
                        Discount = 0,
                        Quantity = 15,
                    }
                }
            },
            new Author
            {
                Name = "Рэй Брэдберри",
                Description = "test desc",
                Books = new List<Book>
                {
                    new Book
                    {
                        Name = "451 градус по Фаренгейту",
                        Description = "test desc",
                        PagesCount = 250,
                        Genre = Genre.Fiction,
                        Language = Language.Russian,
                        Cover = Cover.HardCover,

                        Price = 15,
                        Discount = 0.15,
                        Quantity = 20,

                    },
                    new Book
                    {
                        Name = "Вино из одуванчиков",
                        Description = "test desc",
                        PagesCount = 320,
                        Genre = Genre.Fiction,
                        Language = Language.Russian,
                        Cover = Cover.HardCover,

                        Price = 10,
                        Discount = 0,
                        Quantity = 23,
                    }
                }
            }
        };
    }
}