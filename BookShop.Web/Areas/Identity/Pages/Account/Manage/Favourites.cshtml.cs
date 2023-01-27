using BookShop.DAL.Data;
using BookShop.BLL.Entities.Products;
using BookShop.BLL.Interfaces;
using BookShop.Web.Interfaces;
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
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
        }

        await LoadAsync(user, repository);
        return Page();
    }

    private async Task LoadAsync(ApplicationUser user, IRepository<Book> repository)
    {
        Favourites = await favouriteService.GetFavouritesForUser(user);
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {

        var user = await userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
        }

        await LoadAsync(user!, repository);

        await favouriteService.RemoveFromFavourites(user, id.ToString());
        var item = Favourites.FirstOrDefault(x => x.Id == id)!;
        Favourites.Remove(item);
        item.IsFavourite = false;
        return RedirectToPage();
    }
}