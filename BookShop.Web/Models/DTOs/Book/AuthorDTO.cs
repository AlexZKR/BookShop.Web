namespace BookShop.Web.Models.Book
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string PictureUri { get; set; } = "no_img";
    }
}