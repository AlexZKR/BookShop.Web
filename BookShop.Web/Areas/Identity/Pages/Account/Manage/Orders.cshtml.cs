using BookShop.DAL.Data;
using BookShop.BLL.Entities.Products;
using BookShop.BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookShop.Web.Models.Order;
using BookShop.Web.Services;

namespace BookShop.Web.Areas.Identity.Pages.Account.Manage;

public class OrdersModel : PageModel
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IOrderService orderService;
    private readonly IOrderViewModelService orderViewModelService;
    public List<OrderViewModel> orderViewModels = new List<OrderViewModel>();

    public OrdersModel(UserManager<ApplicationUser> userManager,
     IOrderService orderService,
     IOrderViewModelService orderViewModelService
     )
    {
        this.userManager = userManager;
        this.orderService = orderService;
        this.orderViewModelService = orderViewModelService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var name = Request.HttpContext.User.Identity!.Name;
        if (name == null)
        {
            return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
        }

        await LoadAsync(name);
        return Page();
    }

    private async Task LoadAsync(string user)
    {
        var name = Request.HttpContext.User.Identity!.Name;
        if(name != null)
        {
            var orders = await orderService.GetUserOrdersAsync(name);
            orders.ForEach(async o =>
            orderViewModels.Add(await orderViewModelService.CreateOrderViewModelAsync(o.Id))
            );
        }
    }

    // public async Task<IActionResult> OnPostDeleteAsync(int id)
    // {

    //     var username = Request.HttpContext.User.Identity!.Name;

    //     if (username == null)
    //     {
    //         return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
    //     }

    //     await LoadAsync(username);

    //     await favouriteService.RemoveFromFavourites(username, id.ToString());
    //     var item = Favourites.FirstOrDefault(x => x.Id == id)!;
    //     Favourites.Remove(item);
    //     item.IsFavourite = false;
    //     return RedirectToPage();
    // }
}