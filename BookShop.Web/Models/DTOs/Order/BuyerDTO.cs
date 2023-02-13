namespace BookShop.Web.Models.DTOs.Order;

    public class BuyerDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int Discount { get; set; }
        public string? PhoneNumber { get; set; }
        public int UnproccessedOrdersCount { get; set; }

        //orders are queried separately by buyers id
        //public List<OrderDTO> Orders { get; set; } = new List<OrderDTO>();
    }
