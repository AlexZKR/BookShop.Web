namespace BookShop.BLL.Exceptions;

public class NotFoundInDbException : Exception
{
    public NotFoundInDbException(string message) : base(message)
    {
    }
}
