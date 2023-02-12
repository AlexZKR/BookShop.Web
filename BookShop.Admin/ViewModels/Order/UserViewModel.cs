namespace BookShop.Admin.ViewModels.Order;

public class UserViewModel
{
    public UserViewModel(string id,
                         string name,
                         string email,
                         string phoneNumber)
    {
        Id = id;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public string Id {get; set;}
    public string Name {get; set;}
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public List<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();


}