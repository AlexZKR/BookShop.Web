namespace BookShop.DAL.Exceptions;

public class EmptyBasketOnCheckoutException : Exception
{
    public EmptyBasketOnCheckoutException() : base($"Basket cannot have 0 items on checkout") { }
}