namespace BookShop.Web.Models.DTOs.Order;

    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int Units { get; set; }

    }
