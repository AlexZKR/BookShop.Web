using AutoMapper;
using BookShop.Web.Models;
using BookShop.BLL.Interfaces;
using BookShop.Web.Models.DTOs.Order;
using Microsoft.AspNetCore.Mvc;
using BookShop.Web.Infrastructure;

namespace BookShop.Web.Controllers;
// CRUD apis operations best practices
// https://ardalis.com/web-api-dto-considerations/

// Query filtering
// https://stackoverflow.com/questions/70632225/asp-net-core-web-api-route-by-query-parameter
[Route("api/orders")]
// [ApiController]
public class OrdersAPIController : ControllerBase
{
    private readonly IOrderService orderService;
    private readonly IMapper mapper;
    protected ResponseDTO response;

    public OrdersAPIController(IOrderService orderService, IMapper mapper)
    {
        this.orderService = orderService;
        this.mapper = mapper;
        response = new ResponseDTO();
    }
    //Getting full list of buyers. Then,
    //only unproccessed orders or orders specific to buyer by buyerId
    //[HttpGet("api/orders")]
    //[Route("api/orders")]
    // [HttpGet]
    // public ActionResult<object> Get()
    // {
    //     response.DisplayMessage = "Hello from BookShopOrdersAPI! :)";
    //     return response;
    // }
    ///api/orders/buyers
    [HttpGet("buyers")]
    public async Task<ActionResult<object>> GetBuyers()
    {
        try
        {
            var buyers = (await orderService.GetAllBuyersAsync())
                .Select(o => o.Buyer).ToList();

            var dtos = buyers.Select(o => mapper.Map<BuyerDTO>(o)).DistinctBy(x => x.Id).ToList();
            response.Result = dtos;

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessage = new List<string> { ex.ToString() };
        }
        response.DisplayMessage ="GetBuyers";
        return response;
    }
        ///api/orders?buyer=id
        [HttpGet]
        [HttpGet]
        [QueryParameterConstraint("buyer")]
        // [HttpGet("buyers")]
        public async Task<ActionResult<object>> GetBuyersOrders([FromQuery] string buyer)
        {
            try
            {
                var orderList = await orderService.GetBuyersOrdersAsync(buyer);
                var dtos = orderList.Select(o => mapper.Map<OrderDTO>(o)).ToList();
                response.Result = dtos;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.ToString() };
            }
            response.DisplayMessage ="GetBuyersOrders";
            return response;
        }

        ///api/orders?id=orderId
        // [HttpGet]
        [HttpGet]
        [QueryParameterConstraint("orderId")]
        public async Task<ActionResult<object>> GetOrderDetails([FromQuery] string orderId)
        {
            try
            {
                var order = await orderService.GetOrderByIdAsync(Int32.Parse(orderId));
                var dto = mapper.Map<OrderDTO>(order);
                response.Result = dto;

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.ToString() };
            }
            response.DisplayMessage ="GetOrderDetails";
            return response;
        }
}