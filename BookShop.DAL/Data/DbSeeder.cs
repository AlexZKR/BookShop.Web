using BookShop.BLL.Entities.Enums;
using BookShop.BLL.Entities.Products;
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
            if (!await context.Books.AnyAsync())
            {
                await context.Books.AddRangeAsync(
                    GetPreconfiguredBooks());

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

    public static async Task SeedIdDataAsync(appIdentityDbContext idContext, ILogger logger, int retry = 0)
    {
        var retryForAvailability = retry;
        try
        {
            idContext.Database.EnsureCreated();

            // if (!await idContext.Users.AnyAsync())
            // {
            //     await idContext.Users.AddRangeAsync(
            //         GetPreconfiguredUsers());

            //     await idContext.SaveChangesAsync();
            // }
        }
        catch (Exception ex)
        {
            if (retryForAvailability >= 10) throw;

            retryForAvailability++;

            logger.LogError(ex.Message);
            await SeedIdDataAsync(idContext, logger, retryForAvailability);
            throw;
        }
    }

    private static ApplicationUser[] GetPreconfiguredUsers()
    {
        throw new NotImplementedException();
    }

    private static IEnumerable<Author> GetPreconfiguredAuthors()
    {
        return new List<Author>()
        {
            new Author
            {
                Name = "Олдос Хаксли",
                Description = "test desc",
            },
            new Author
            {
                Name = "Рэй Брэдберри",
                Description = "test desc",
            },
            new Author
            {
                Name = "Юваль Ной Харари",
                Description = "израильский военный историк-медиевист, профессор исторического факультета Еврейского университета в Иерусалиме, автор международного бестселлера «Sapiens: Краткая история человечества».",
            },
            new Author
            {
                Name = "Ольга Примаченко",
                Description = "белорусская журналистка, психолог, блогер, автор вдохновляющих книг о самопознании и гармоничных отношениях с собой и окружающими.",
            }
        };
    }

    private static IEnumerable<Book> GetPreconfiguredBooks()
    {
        return new List<Book>()
        {
            new Book
                    {
                        Name = "О дивный новый мир",
                        Description = "test desc",
                        PagesCount = 320,

                        Genre = Genre.Fiction,
                        Language = Language.Russian,
                        Cover = Cover.HardCover,

                        Tag = Tag.Classic,

                        FullPrice = 12,
                        Discount = 0.10,
                        Quantity = 10,

                        Sold = 453,

                        ImagePath = "b_01.jpg",

                        AuthorId = 1,
                    },
                    new Book
                    {
                        Name = "Остров",
                        Description = "test desc",
                        PagesCount = 250,

                        Genre = Genre.Fiction,
                        Language = Language.Russian,
                        Cover = Cover.HardCover,

                        Tag = Tag.None,

                        FullPrice = 8,
                        Discount = 0,
                        Quantity = 15,

                        Sold = 34,

                        ImagePath = "b_02.jpg",

                        AuthorId = 1
                    },
                    new Book
                    {
                        Name = "451 градус по Фаренгейту",
                        Description = "test desc",
                        PagesCount = 250,


                        Genre = Genre.Fiction,
                        Language = Language.Russian,
                        Cover = Cover.HardCover,

                        Tag = Tag.Bestseller,

                        FullPrice = 15,
                        Discount = 0.15,
                        Quantity = 20,

                        Sold = 200,

                        ImagePath = "b_03.jpg",
                        AuthorId = 2
                    },
                    new Book
                    {
                        Name = "Вино из одуванчиков",
                        Description = "test desc",
                        PagesCount = 320,


                        Genre = Genre.Fiction,
                        Language = Language.Russian,
                        Cover = Cover.HardCover,

                        Tag = Tag.Classic,

                        FullPrice = 10,
                        Discount = 0,
                        Quantity = 23,

                        Sold = 145,

                        ImagePath = "b_04.jpg",
                        AuthorId = 2
                    },
                    new Book
                    {
                        Name = "Sapiens: Краткая история человечества",
                        Description = "книга профессора Юваля Ноя Харари, впервые опубликованная на иврите в Израиле в 2011 году, а на английском языке в 2014 году.",
                        PagesCount = 460,


                        Genre = Genre.ScienceFiction,
                        Language = Language.English,
                        Cover = Cover.SuperCover,

                        Tag = Tag.Bestseller,

                        FullPrice = 30,
                        Discount = 0.14,
                        Quantity = 45,

                        Sold = 950,

                        ImagePath = "b_05.jpg",
                        AuthorId = 3
                    },
                    new Book
                    {
                        Name = "К себе нежно. Книга о том, как ценить и беречь себя",
                        Description = "\"К себе нежно\" – это новый, очень честный взгляд на любовь к себе. Это книга-медитация, которая призывает к внутреннему разговору и помогает услышать собственный голос среди множества других.",
                        PagesCount = 460,


                        Genre = Genre.Psychology,
                        Language = Language.Belarusian,
                        Cover = Cover.SoftCover,

                        Tag = Tag.None,

                        FullPrice = 25,
                        Discount = 0.05,
                        Quantity = 70,

                        Sold = 120,

                        ImagePath = "b_06.jpg",
                        AuthorId = 4
                    }
        };
    }
}
