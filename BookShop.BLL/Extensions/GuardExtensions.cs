using Ardalis.GuardClauses;
using BookShop.BLL.Entities.Basket;
using BookShop.BLL.Exceptions;

namespace BookShop.BLL.Extensions;

public static class BasketGuards
{
    public static void EmptyBasketOnCheckout(this IGuardClause guardClause, IReadOnlyCollection<BasketItem> basketItems)
    {
        if (!basketItems.Any())
            throw new EmptyBasketOnCheckoutException();
    }
}
