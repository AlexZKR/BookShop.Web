using BookShop.DAL.Data;
using BookShop.DAL.Entities;
using BookShop.DAL.Entities.Products;
using BookShop.Web.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookShop.Web.Areas.Identity.Pages.Account.Manage;

public class FavouritesModel : PageModel
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IFavouriteService<Book> favouriteService;
    private readonly AppDbContext context;

    public List<Book> Favourites { get; set; } = new List<Book>();

    public FavouritesModel(UserManager<ApplicationUser> userManager,
     IFavouriteService<Book> favouriteService,
     AppDbContext context)
    {
        this.userManager = userManager;
        this.favouriteService = favouriteService;
        this.context = context;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
        }

        await LoadAsync(user, context);
        return Page();
    }

    private async Task LoadAsync(ApplicationUser user, AppDbContext context)
    {
        Favourites = await favouriteService.GetFavouritesForUser(user, context);
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {

        var user = await userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
        }

        await LoadAsync(user!, context);

        await favouriteService.RemoveFromFavourites(user, id.ToString());
        var item = Favourites.FirstOrDefault(x => x.Id == id)!;
        Favourites.Remove(item);
        item.IsFavourite = false;
        return RedirectToPage();
    }
}