using BookShop.Admin.Models.Product;

namespace BookShop.Admin.ViewModels.Catalog
{
    public class ProductViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int PagesCount { get; set; }
        public Genre Genre { get; set; }
        public Cover Cover { get; set; }
        public Language Language { get; set; }
        public Tag Tag { get; set; }
        public double FullPrice { get; set; }
        public double Discount { get; set; }
        public double DiscountedPrice => FullPrice - (FullPrice * Discount);
        public int Quantity { get; set; }
        public int Sold { get; set; }
        public string? PictureUri { get; set; }
        public double Rating { get; set; }
        public int AuthorId { get; set; }

    }
}