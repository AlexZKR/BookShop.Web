using AutoMapper;
using BookShop.Admin.Interfaces;
using BookShop.Admin.Models;
using BookShop.Admin.Models.Order;
using BookShop.Admin.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookShop.Web.Controllers;

public class OrdersController : Controller
{
    private readonly IOrderService orderService;
    private readonly IMapper mapper;

    public OrdersController(IOrderService orderService,
    IMapper mapper)
    {
        this.orderService = orderService;
        this.mapper = mapper;
    }

    //TODO: service for creating vms
    public async Task<IActionResult> Index()
    {
        var vm = new OrdersPageViewModel();

        var response = await orderService.GetAllBuyersAsync<ResponseDTO>();
        if(response != null && response.IsSuccess)
        {
            var list = JsonConvert.DeserializeObject<List<BuyerDTO>>(Convert.ToString(response.Result)!);
            vm.Buyers = list!.Select(d => mapper.Map<BuyerViewModel>(d)).ToList();
        }
        else
        {
            vm.StatusMessage = "Error loading data. Try later.";
            return View(vm);
        }

        //TODO: add for user specific orders
        if(vm.Buyers.Count == 0) vm.StatusMessage = "Nothing found";

        return View(vm);
    }
    //get list of orders by buyerId
    [HttpGet]
    public async Task<IActionResult> GetOrders(string buyerId, string buyerName, int count)
    {
        var response = await orderService.GetUserWithOrdersByIdAsync<ResponseDTO>(buyerId);
        var vm = new OrdersPageViewModel();

        if(response != null && response.IsSuccess)
        {
            var ordersDTOs = JsonConvert.DeserializeObject<List<OrderDTO>>(Convert.ToString(response.Result)!);
            var orders = ordersDTOs!.Select(o => mapper.Map<OrderViewModel>(o)).ToList();

            vm.ProccessedOrders.AddRange(orders.Where(o => o.IsProccessed == true));
            vm.UnproccessedOrders.AddRange(orders.Where(o => o.IsProccessed == false));

            vm.BuyerName = buyerName;
            vm.UnproccessedOrdersCount = count;
        }
        else
        {
            vm.StatusMessage = "Error loading data. Try later.";
            //return PartialView("_OrdersMenuPartial",vm);
        }

        //TODO: add for user specific orders
        //if(vm.Buyers.Count == 0) vm.StatusMessage = "Nothing found";

        return PartialView("_OrdersMenuPartial", vm);
    }
}