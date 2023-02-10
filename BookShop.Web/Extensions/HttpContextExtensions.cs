namespace BookShop.Web.Extensions;

public static class HttpContextExtensions
{
    public static string GetUsername(this HttpContext httpContext)
    {
        var user = httpContext.User;
        if (user.Identity == null) throw new NullReferenceException();
        string? userName = null;

        if (user.Identity.IsAuthenticated)
        {
            if (user.Identity.Name != null)
                return user.Identity.Name!;
        }
        userName = Guid.NewGuid().ToString();
        return userName!;
    }
}