using BookShop.DAL.Entities.Products;

namespace BookShop.DAL.Entities.Order;

public class OrderItem : BaseEntity
{
    public Book ItemOrdered { get; set; }

    public OrderItem(Book itemOrdered)
    {
        ItemOrdered = itemOrdered;
    }
}