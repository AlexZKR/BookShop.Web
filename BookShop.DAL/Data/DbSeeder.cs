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
                Description = "профессор исторического факультета Еврейского университета в Иерусалиме",
            },
            new Author
            {
                Name = "Ольга Примаченко",
                Description = "белорусская журналистка, психолог, блогер, автор вдохновляющих книг о самопознании и гармоничных отношениях с собой и окружающими.",
            },
            new Author
            {
                Name = "Эрих Мария Ремарк",
                Description = "",
            },
            new Author
            {
                Name = "Братья Стругацкие",
                Description = "",
            },


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

                        PictureUri = "b_01.jpg",

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

                        PictureUri = "b_02.jpg",

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

                        PictureUri = "b_03.jpg",
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

                        PictureUri = "b_04.jpg",
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

                        PictureUri = "b_05.jpg",
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

                        PictureUri = "b_06.jpg",
                        AuthorId = 4
                    },
                    new Book
                    {
                        Name = "Триумфальная арка",
                        Description = "Триумфальная арка пронзительная история любви всему наперекор, любви, приносящей боль, но и дарующей бесконечную радость.",
                        PagesCount = 460,


                        Genre = Genre.Fiction,
                        Language = Language.Russian,
                        Cover = Cover.SoftCover,

                        Tag = Tag.Bestseller,

                        FullPrice = 20,
                        Discount = 0.1,
                        Quantity = 150,

                        Sold = 120,

                        PictureUri = "b_07.jpg",
                        AuthorId = 5
                    },
                    new Book
                    {
                        Name = "Жизнь взаймы",
                        Description = "Жизнь взаймы это жизнь, которую герои отвоевывают у смерти. Когда терять уже нечего, когда один стоит на краю гибели, так эту жизнь и не узнав, а другому эта треклятая жизнь стала невыносима.",
                        PagesCount = 460,


                        Genre = Genre.Fiction,
                        Language = Language.Russian,
                        Cover = Cover.SoftCover,

                        Tag = Tag.None,

                        FullPrice = 15,
                        Discount = 0.2,
                        Quantity = 70,

                        Sold = 120,

                        PictureUri = "b_08.jpg",
                        AuthorId = 5
                    },
                    new Book
                    {
                        Name = "Три товарища",
                        Description = "В биографии одного из самых известных немецких писателей Эриха Марии Ремарка значатся обучение в католической семинарии, участие в Первой мировой войне, работа продавцом надгробий, бессчетное количество интрижек и написание книг, которые навсегда изменили его жизнь и стали мировой классикой",
                        PagesCount = 460,


                        Genre = Genre.Fiction,
                        Language = Language.Russian,
                        Cover = Cover.SoftCover,

                        Tag = Tag.Bestseller,

                        FullPrice = 30,
                        Discount = 0.0,
                        Quantity = 70,

                        Sold = 120,

                        PictureUri = "b_09.jpg",
                        AuthorId = 5
                    },
                    new Book
                    {
                        Name = "На Западном фронте без перемен",
                        Description = "Говоря о Первой мировой войне, всегда вспоминают одно произведение Эриха Марии Ремарка.На Западном фронте без перемен.",
                        PagesCount = 320,


                        Genre = Genre.Fiction,
                        Language = Language.Russian,
                        Cover = Cover.SoftCover,

                        Tag = Tag.None,

                        FullPrice = 19,
                        Discount = 0.3,
                        Quantity = 70,

                        Sold = 250,

                        PictureUri = "b_10.jpg",
                        AuthorId = 5
                    },
                    new Book
                    {
                        Name = "На обратном пути",
                        Description = "Ах, как трудно прощаться! Но возвращаться иногда еще труднееСпустя четыре тяжелых года война, наконец, закончилась Эрнст и его фронтовые товарищи возвращаются домой в город, который некогда покинули еще детьми",
                        PagesCount = 500,


                        Genre = Genre.Fiction,
                        Language = Language.Russian,
                        Cover = Cover.SoftCover,

                        Tag = Tag.Classic,

                        FullPrice = 15,
                        Discount = 0.03,
                        Quantity = 70,

                        Sold = 120,

                        PictureUri = "b_11.jpg",
                        AuthorId = 5
                    },
                    new Book
                    {
                        Name = "Пикник на обочине",
                        Description = "Пожалуй, в истории современной мировой фантастики найдется не так много произведений, которые оставались бы популярными столь долгое время.",
                        PagesCount = 320,


                        Genre = Genre.Fiction,
                        Language = Language.Russian,
                        Cover = Cover.SoftCover,

                        Tag = Tag.Bestseller,

                        FullPrice = 12,
                        Discount = 0.2,
                        Quantity = 200,

                        Sold = 120,

                        PictureUri = "b_12.jpg",
                        AuthorId = 6
                    },
                    new Book
                    {
                        Name = "Понедельник начинается в субботу",
                        Description = "Понедельник начинается в субботу. Сказка для научных работников младшего возраста под таким заголовком в 1965 году вышла книга, которой зачитывались и продолжают зачитываться все новые и новые поколения.",
                        PagesCount = 250,


                        Genre = Genre.Fiction,
                        Language = Language.Russian,
                        Cover = Cover.SoftCover,

                        Tag = Tag.None,

                        FullPrice = 25,
                        Discount = 0.05,
                        Quantity = 70,

                        Sold = 250,

                        PictureUri = "b_13.jpg",
                        AuthorId = 6
                    },
                    new Book
                    {
                        Name = "Трудно быть богом",
                        Description = "Трудно быть богом.Возможно, самое известное из произведений братьев Стругацких. Один из самых прославленных повестей отечественной фантастики.",
                        PagesCount = 520,


                        Genre = Genre.Fiction,
                        Language = Language.Russian,
                        Cover = Cover.SoftCover,

                        Tag = Tag.None,

                        FullPrice = 13,
                        Discount = 0.05,
                        Quantity = 70,

                        Sold = 120,

                        PictureUri = "b_14.jpg",
                        AuthorId = 6
                    },
                    new Book
                    {
                        Name = "Улитка на склоне",
                        Description = "Одно из самых загадочных и провокационных произведений братьев Стругацких, печатавшееся по частям в 1966 и 1968 годах, после чего попало под запрет и пришло к читателю в полном объеме только в 1988 году.",
                        PagesCount = 475,


                        Genre = Genre.Fiction,
                        Language = Language.Russian,
                        Cover = Cover.SoftCover,

                        Tag = Tag.None,

                        FullPrice = 16,
                        Discount = 0.05,
                        Quantity = 70,

                        Sold = 120,

                        PictureUri = "b_15.jpg",
                        AuthorId = 6
                    },
                    new Book
                    {
                        Name = "Забытый эксперимент",
                        Description = "В сборнике представлены ранние произведения братьев Стругацких рассказы и маленькие повести, относящиеся к разным жанрам, стилям и направлениям фантастики, от классической НФ до философской притчи.",
                        PagesCount = 460,


                        Genre = Genre.Fiction,
                        Language = Language.Russian,
                        Cover = Cover.SoftCover,

                        Tag = Tag.None,

                        FullPrice = 8,
                        Discount = 0.05,
                        Quantity = 70,

                        Sold = 50,

                        PictureUri = "b_16.jpg",
                        AuthorId = 6
                    },
        };
    }
}
