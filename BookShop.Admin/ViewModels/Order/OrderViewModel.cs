namespace BookShop.Admin.ViewModels.Order;

public class OrderViewModel
{
    public int Id {get; set;}
    public string? UserId {get; set;}
    public DateTime OrderDate {get; set;}
    public bool IsProccessed {get; set;}


}