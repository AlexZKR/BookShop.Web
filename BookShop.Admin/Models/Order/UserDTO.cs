namespace BookShop.Admin.Models.Order
{
    public class UserDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int Discount { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public List<OrderDTO> Orders { get; set; } = new List<OrderDTO>();
    }
}