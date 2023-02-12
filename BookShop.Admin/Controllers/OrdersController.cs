using BookShop.Admin.Interfaces;
using BookShop.Admin.Models;
using BookShop.Admin.Models.Order;
using BookShop.Admin.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookShop.Web.Controllers;

public class OrdersController : Controller
{
    private readonly IEntityService entityService;

    public OrdersController(IEntityService entityService)
    {
        this.entityService = entityService;
    }

    //TODO: service for creating vms
    public async Task<IActionResult> Index()
    {
        var list = new List<OrderDTO>();
        var vm = new OrdersPageViewModel
        {
            Users = new List<UserViewModel>(),
        };
        var response = await entityService.GetAllEntitiesAsync<ResponseDTO>();
        if(response != null && response.IsSuccess)
        {
            list = JsonConvert.DeserializeObject<List<OrderDTO>>(Convert.ToString(response.Result)!);
            vm.Orders = list!.Select(i => new OrderViewModel
            {
                Id = i.OrderId,
                BuyerId = i.BuyerId,
                OrderDate = i.OrderDate,
                IsProccessed = i.IsProccessed,

                ItemViewModels = i.OrderItems.Select(o => new OrderItemViewModel
                {
                    Id = o.Id,
                    Name = o.Name,
                    FullPrice = o.Price,
                    Discount = o.Discount,
                    Units = o.Units,
                }).ToList(),
            }).ToList();
        }
        else
        {
            vm.StatusMessage = "Error loading orders. Try later.";
            return View(vm);
        }

        //TODO: add for user specific orders
        if(vm.Orders.Count == 0) vm.StatusMessage = "Nothing found";

        return View(vm);
    }
}