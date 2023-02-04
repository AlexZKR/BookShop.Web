using Ardalis.GuardClauses;
using BookShop.BLL;
using BookShop.BLL.Interfaces;
using BookShop.DAL.Data;
using BookShop.Web.Interfaces;
using BookShop.Web.Models.Order;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers;

public class OrderController : Controller
{
    private readonly IOrderService orderService;
    private readonly IOrderViewModelService orderViewModelService;
    private readonly IBasketService basketService;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IAppLogger<OrderController> logger;
    private readonly IBasketViewModelService basketViewModelService;
    private string? username = null;

    public OrderController(IOrderService orderService,
    IOrderViewModelService orderViewModelService,
    IBasketService basketService,
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager,
    IAppLogger<OrderController> logger,
    IBasketViewModelService basketViewModelService)
    {
        this.orderService = orderService;
        this.orderViewModelService = orderViewModelService;
        this.basketService = basketService;
        this.signInManager = signInManager;
        this.userManager = userManager;
        this.logger = logger;
        this.basketViewModelService = basketViewModelService;
    }

    public async Task<IActionResult> Index()
    {
        var user = await userManager.GetUserAsync(HttpContext.User);
        var orderVM = await SetBasketVMAsync();
        if (user != null)
        {
            orderVM.FirstName = user.FirstName;
            orderVM.LastName = user.LastName;
            orderVM.Email = user.Email;
            orderVM.PhoneNumber = user.PhoneNumber;
        }


        return View(orderVM);
    }

    //private helpers
    private async Task<OrderViewModel> SetBasketVMAsync()
    {
        var vm = new OrderViewModel();
        //Guard.Against.Null(User?.Identity?.Name, nameof(User.Identity.Name));
        if (signInManager.IsSignedIn(HttpContext.User))
        {
            vm = await orderViewModelService.CreateOrderVMAsync(User.Identity.Name);
        }
        else
        {
            GetOrSetBasketCookieAndUserName();
            vm = await orderViewModelService.CreateOrderVMAsync(username!);
        }
        return vm;
    }

    private void GetOrSetBasketCookieAndUserName()
    {
        if (Request.Cookies.ContainsKey(SD.BASKET_COOKIENAME))
        {
            username = Request.Cookies[SD.BASKET_COOKIENAME];
        }
        if (username != null) return;

        username = Guid.NewGuid().ToString();
        var cookieOptions = new CookieOptions();
        cookieOptions.Expires = DateTime.Today.AddYears(10);
        Response.Cookies.Append(SD.BASKET_COOKIENAME, username, cookieOptions);
    }
}