using BookShop.BLL;
using BookShop.BLL.Entities.Enums;
using BookShop.BLL.Entities.Order;
using BookShop.BLL.Interfaces;
using BookShop.DAL.Data;
using BookShop.Web.Interfaces;
using BookShop.Web.Models.Order;
using BookShop.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers;
[Authorize]
[Route("Order")]
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
    private readonly IOrderViewModelService orderViewModelService;
    private string? username = null;

    public OrderController(IOrderService orderService,
    ICheckOutViewModelService checkOutViewModelService,
    IBasketService basketService,
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager,
    IAppLogger<OrderController> logger,
    IBasketViewModelService basketViewModelService,
    IOrderViewModelService orderViewModelService)
    {
        this.orderService = orderService;
        this.checkOutViewModelService = checkOutViewModelService;
        this.basketService = basketService;
        this.signInManager = signInManager;
        this.userManager = userManager;
        this.logger = logger;
        this.basketViewModelService = basketViewModelService;
        this.orderViewModelService = orderViewModelService;
    }
    [Route("Index")]
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
        GetOrSetBasketCookieAndUserName();
        MapCheckoutVm(vm, out Address address, out Buyer buyer, out OrderInfo orderInfo);
        Order order = await orderService.CreateOrderAsync(address, buyer, orderInfo);
        var orderVm = await orderViewModelService.CreateOrderViewModelAsync(order.Id);
        return View("OrderDetails",orderVm);
    }

    // [HttpGet("{orderId:int}")]
    [Route("[action]/{orderId:int}")]

    public async Task<IActionResult> OrderDetails(int orderId)
    {
        var orderVm = await orderViewModelService.CreateOrderViewModelAsync(orderId);
        return View(orderVm);
    }


    //private helpers
    private async Task<CheckOutViewModel> SetCheckOutVMAsync()
    {
        var vm = new CheckOutViewModel();

        if (signInManager.IsSignedIn(HttpContext.User))
        {
            vm = await checkOutViewModelService.CreateCheckOutVMAsync(User.Identity!.Name!);
        }
        else
        {
            GetOrSetBasketCookieAndUserName();
            vm = await checkOutViewModelService.CreateCheckOutVMAsync(username!);
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

    private void MapCheckoutVm(CheckOutViewModel vm, out Address address, out Buyer buyer, out OrderInfo orderInfo)
    {
        address = new Address
        {
            Street = vm.Street,
            Region = (Region)vm.Region,
            City = vm.City,
            PostCode = vm.PostCode
        };

        buyer = new Buyer
        {
            BuyerId = vm.BuyerId,
            FirstName = vm.FirstName,
            LastName = vm.LastName,
            PhoneNumber = vm.PhoneNumber,
            Email = vm.Email
        };

        orderInfo = new OrderInfo
        {
            PaymentType = (PaymentType)vm.PaymentType,
            DeliveryType = (DeliveryType)vm.DeliveryType,
            OrderComment = vm.OrderComment
        };
    }
}