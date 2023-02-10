using Ardalis.GuardClauses;
using BookShop.BLL;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Extensions;

public static class ControllerBaseExtensions
{
//Even unauth users can create their cart. If they want to proceed with it, they will have to register
    public static string GetOrSetBasketCookieAndUserName(this ControllerBase controller)
    {
        Guard.Against.Null(controller.Request.HttpContext.User.Identity, nameof(controller.Request.HttpContext.User.Identity));
        string? userName = null;

        if (controller.Request.HttpContext.User.Identity.IsAuthenticated)
        {
            Guard.Against.Null(controller.Request.HttpContext.User.Identity.Name, nameof(controller.Request.HttpContext.User.Identity.Name));
            return controller.Request.HttpContext.User.Identity.Name!;
        }

        if (controller.Request.Cookies.ContainsKey(SD.BASKET_COOKIENAME))
        {
            userName = controller.Request.Cookies[SD.BASKET_COOKIENAME];

            if (!controller.Request.HttpContext.User.Identity.IsAuthenticated)
            {
                if (!Guid.TryParse(userName, out var _))
                {
                    userName = null;
                }
            }
        }
        if (userName != null) return userName;

        userName = Guid.NewGuid().ToString();
        var cookieOptions = new CookieOptions { IsEssential = true };
        cookieOptions.Expires = DateTime.Today.AddYears(10);
        controller.Response.Cookies.Append(SD.BASKET_COOKIENAME, userName, cookieOptions);

        return userName;
    }

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