using BookShop.DAL.Data;
using BookShop.BLL.Entities.Products;
using BookShop.BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookShop.Web.Areas.Identity.Pages.Account.Manage;

public class FavouritesModel : PageModel
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IFavouriteService<Book> favouriteService;
    private readonly IRepository<Book> repository;

    public List<Book> Favourites { get; set; } = new List<Book>();

    public FavouritesModel(UserManager<ApplicationUser> userManager,
     IFavouriteService<Book> favouriteService,
     IRepository<Book> repository)
    {
        this.userManager = userManager;
        this.favouriteService = favouriteService;
        this.repository = repository;
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
        Favourites = await favouriteService.GetFavouritesForUser(name!);
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {

        var username = Request.HttpContext.User.Identity!.Name;

        if (username == null)
        {
            return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
        }

        await LoadAsync(username);

        await favouriteService.RemoveFromFavourites(username, id.ToString());
        var item = Favourites.FirstOrDefault(x => x.Id == id)!;
        Favourites.Remove(item);
        item.IsFavourite = false;
        return RedirectToPage();
    }
}