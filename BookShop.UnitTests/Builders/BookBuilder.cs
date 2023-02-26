using BookShop.BLL.Entities.Products;

namespace BookShop.UnitTests.Buidlers;

public class BookBuilder
{
    private Book book;
    public string testName = "testName";
    public string testDesc = "testDesc";
    public string testPictureUri = "b_01.jpg";
    public double testFullPrice = 10;
    public double testDiscount = 0.1;
    public double testRating = 5;
    public int testQuantity = 2;
    public int testSold = 200;

    public BookBuilder()
    {
        book = new Book
        {
            Name = testName,
            Description = testDesc,
            FullPrice = testFullPrice,
            Discount = testDiscount,
            Quantity = testQuantity,
            Sold = testSold,
            Rating = testRating,
            PagesCount = 1000,
            Genre = BookShop.BLL.Entities.Enums.Genre.Fiction,
            Language = BookShop.BLL.Entities.Enums.Language.English,
            Cover = BookShop.BLL.Entities.Enums.Cover.SuperCover,
            Tag = BookShop.BLL.Entities.Enums.Tag.Classic,
            Author = new Author
            {
                Name = "testAuthorName",
                Description = "testAuthorDesc",
                PictureUri = "testAuthPic",
            }
        };
    }

    public Book WithDefaultValues()
    {
        return book;
    }

}
