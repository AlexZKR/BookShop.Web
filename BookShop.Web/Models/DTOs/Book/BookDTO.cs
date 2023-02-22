namespace BookShop.Web.Models.Book;

public class BookDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int PagesCount { get; set; }

    //enums
    public string? Genre { get; set; }
    public string? Cover { get; set; }
    public string? Language { get; set; }
    public string? Tag { get; set; } = "none";
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
