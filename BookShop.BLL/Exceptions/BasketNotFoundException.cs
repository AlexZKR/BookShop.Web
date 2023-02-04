namespace BookShop.BLL.Exceptions;

public class BasketNotFoundException : Exception
{
    public BasketNotFoundException(int basketId) : base($"No basket found with id {basketId}")
    {
    }
}
