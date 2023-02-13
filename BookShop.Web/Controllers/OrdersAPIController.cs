using AutoMapper;
using BookShop.Web.Models;
using BookShop.BLL.Interfaces;
using BookShop.Web.Models.DTOs.Order;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers;
// CRUD apis operations best practices
// https://ardalis.com/web-api-dto-considerations/

[Route("api/orders")]
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
        return response;
    }
        ///api/orders?id=id
        [HttpGet]
        public async Task<ActionResult<object>> GetBuyersOrders([FromQuery] string id)
        {
            try
            {
                var orderList = await orderService.GetBuyersOrdersAsync(id);
                var dtos = orderList.Select(o => mapper.Map<OrderDTO>(o)).ToList();
                response.Result = dtos;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.ToString() };
            }
            return response;
        }
}