namespace BookShop.Admin.Models.Order
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }
    }
}