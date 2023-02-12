using BookShop.BLL.Entities.DTO;
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
    protected ResponseDTO response;

    public OrdersAPIController(IOrderService orderService)
    {
        this.orderService = orderService;
        response = new ResponseDTO();
    }

    [HttpGet]
    public async Task<ActionResult<object>> Get()
    {
        try
        {
            //should not be mapped inside controller
            var orders = await orderService.GetAllOrdersAsync();

            var dtos = orders.Select(o => new OrderDTO
            {
                OrderId = o.Id,
                BuyerId = o.Buyer.BuyerId,
                IsProccessed = o.IsInProcess,
                OrderDate = o.OrderInfo.OrderDate,
                OrderItems = o.OrderItems.Select(i => new OrderItemDTO
                {
                    Id= i.Id,
                    OrderId = i.OrderId,
                    Name = i.ProductName,
                    Price = i.FullPrice,
                    Discount = i.Discount,
                    Units = i.Units
                }).ToList(),
            }).ToList();

            response.Result = dtos;

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessage = new List<string> { ex.ToString() };
        }
        return response;
    }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<object>> Get(int id)
        {
            try
            {
                var order = await orderService.GetOrderByIdAsync(id);
                OrderDTO dto = new OrderDTO
                {
                    OrderId = order.Id,
                    BuyerId = order.Buyer.BuyerId,
                    OrderDate = order.OrderInfo.OrderDate,
                    OrderItems = order.OrderItems.Select(i => new OrderItemDTO
                    {
                        Id = i.Id,
                        OrderId = i.OrderId,
                        Price = i.FullPrice,
                        Discount = i.Discount,
                        Name = i.ProductName,
                    }).ToList(),
                };
                response.Result = dto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string> { ex.ToString() };
            }
            return response;
        }
}