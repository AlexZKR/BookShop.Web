using BookShop.BLL;
using BookShop.BLL.Entities.Order;
using BookShop.BLL.Interfaces;
using BookShop.DAL.Data;
using BookShop.Web.Interfaces;
using BookShop.Web.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers;

public class OrderController : Controller
{
    private const string ORDER_VM_BINDING_ATTRIBUTES = "OrderItems, OrderDate, TotalPrice, FullPrice, DiscountSize, TotalItems, DeliveryType, IsInProccess, OrderComment, BuyerId, FirstName, LastName, PhoneNumber, Email, Street, City, PostCode, Region, PaymentType, DeliveryType";
    private readonly IOrderService orderService;
    private readonly ICheckOutViewModelService checkOutViewModelService;
    private readonly IBasketService basketService;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IAppLogger<OrderController> logger;
    private readonly IBasketViewModelService basketViewModelService;
    private string? username = null;

    public OrderController(IOrderService orderService,
    ICheckOutViewModelService checkOutViewModelService,
    IBasketService basketService,
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager,
    IAppLogger<OrderController> logger,
    IBasketViewModelService basketViewModelService)
    {
        this.orderService = orderService;
        this.checkOutViewModelService = checkOutViewModelService;
        this.basketService = basketService;
        this.signInManager = signInManager;
        this.userManager = userManager;
        this.logger = logger;
        this.basketViewModelService = basketViewModelService;
    }
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var user = await userManager.GetUserAsync(HttpContext.User);
        var checkVM = await SetCheckOutVMAsync();
        if (user != null)
        {
            checkVM.FirstName = user.FirstName;
            checkVM.LastName = user.LastName;
            checkVM.Email = user.Email;
            checkVM.PhoneNumber = user.PhoneNumber;
        }

        return View(checkVM);
    }

    [Route("PlaceOrder")]
    [HttpPost("PlaceOrder")]
    public async Task<IActionResult> PlaceOrder([Bind(ORDER_VM_BINDING_ATTRIBUTES)] CheckOutViewModel vm)
    {
        // var items = vm.OrderItems.Select(i => new OrderItem()
        // {
        //     ProductId = i.ProductId,
        //     ProductName = i.ProductName,
        //     FullPrice = i.FullPrice,
        //     DiscountedPrice = i.DiscountedPrice,
        //     Discount = i.Discount,
        //     Units = i.Units,
        //     PictureUrl = i.PictureUrl,
        //     AddInfo = i.AddInfo
        // }).ToList();

        // Order order = await orderService.CreateOrderAsync(totalPrice: vm.TotalPrice,
        //  discountSize: vm.DiscountSize, orderComment: vm.OrderComment!, buyerId: vm.BuyerId!, firstName: vm.FirstName!,
        // lastName: vm.LastName!, phoneNumber: vm.PhoneNumber!, email: vm.Email!, street: vm.Street!,
        // city: vm.City!, postCode: vm.PostCode!, region: vm.Region, paymentType: vm.PaymentType, deliveryType: vm.DeliveryType);
        GetOrSetBasketCookieAndUserName();
        Order order = await orderService.CreateOrderAsync(username);
        return View("OrderDetails", order);
    }


    //private helpers
    private async Task<CheckOutViewModel> SetCheckOutVMAsync()
    {
        var vm = new CheckOutViewModel();

        if (signInManager.IsSignedIn(HttpContext.User))
        {
            vm = await checkOutViewModelService.CreateOrderVMAsync(User.Identity!.Name!);
        }
        else
        {
            GetOrSetBasketCookieAndUserName();
            vm = await checkOutViewModelService.CreateOrderVMAsync(username!);
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