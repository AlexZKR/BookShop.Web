using System.Linq;
using System.Threading.Tasks;
using BookShop.DAL.Data;
using BookShop.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Web.Services;

public class BasketQueryService : IBasketQueryService
{
    private readonly AppDbContext context;

    public BasketQueryService(AppDbContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// This method performs the sum on the database rather than in memory
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public async Task<int> CountTotalBasketItems(string username)
    {
        var totalItems = await context.Baskets
            .Where(basket => basket.BuyerId == username)
            .SelectMany(item => item.Items)
            .SumAsync(sum => sum.Quantity);

        return totalItems;
    }
}
