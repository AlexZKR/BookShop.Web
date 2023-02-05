namespace BookShop.BLL.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(int productId) : base($"No product found with id {productId}")
    {
    }

}
