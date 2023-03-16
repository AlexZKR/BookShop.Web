namespace BookShop.Web.Models.Book;

public class BookDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int PagesCount { get; set; }

    //enums
    public int? Genre { get; set; }
    public int? Cover { get; set; }
    public int? Language { get; set; }
    public int? Tag { get; set; }
    public int AuthorId { get; set; }
    //public AuthorDTO? AuthorDTO { get; set; }

    //$$
    public double FullPrice { get; set; } = 0;
    public double Discount { get; set; } = 0;
    public int Quantity { get; set; } = 0;
    public int Sold { get; set; }
    public string? PictureUri { get; set; } = "no_img";
    public double Rating { get; set; }
}
