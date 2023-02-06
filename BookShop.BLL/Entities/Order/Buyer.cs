
namespace BookShop.BLL.Entities.Order;

public class Buyer //Value object
{
    public Buyer()
    {
    }

    public Buyer(string? BuyerId,
                 string? FirstName,
                 string? LastName,
                 string? PhoneNumber,
                 string? Email)
    {
        this.BuyerId = BuyerId;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.PhoneNumber = PhoneNumber;
        this.Email = Email;
    }

    public string? BuyerId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }


}