using BookShop.BLL.Entities.Enums;
using BookShop.BLL.Entities.Order;
using BookShop.BLL.Exceptions;
using BookShop.BLL.Interfaces;
using BookShop.BLL.Specifications.OrderSpecifications;
using BookShop.Web.Infrastructure;
using BookShop.Web.Models.Order;

namespace BookShop.Web.Services;

public class OrderViewModelService : IOrderViewModelService
{
    private readonly IRepository<Order> orderRepository;

    public OrderViewModelService(IRepository<Order> orderRepository)
    {
        this.orderRepository = orderRepository;
    }
    public async Task<OrderViewModel> CreateOrderViewModelAsync(int orderId)
    {
        var order = await GetOrderWithItemsAsync(orderId);
        var vm = MapOrderToViewModel(order);
        return vm;
    }


    //private helpers
    private async Task<Order> GetOrderWithItemsAsync(int orderId)
    {
        var spec = new OrderWithItemsByIdSpecification(orderId);
        var order = await orderRepository.FirstOrDefaultAsync(spec);
        if (order == null) throw new NotFoundException($"Order with id {orderId} not found in db");
        return order;
    }

    private OrderViewModel MapOrderToViewModel(Order order)
    {
        return new OrderViewModel
        {
            OrderId = order.Id,
            BuyerId = order.Buyer.BuyerId,
            TotalPrice = order.TotalPrice,
            TotalDiscount = order.TotalDiscount,
            BuyerFirstName = order.Buyer.FirstName,
            BuyerLastName = order.Buyer.LastName,
            Email = order.Buyer.Email,
            PhoneNumber = order.Buyer.PhoneNumber,
            Region = EnumHelper<Region>.GetDisplayValue(order.Address.Region),
            City = order.Address.City,
            Street = order.Address.Street,
            PostCode = order.Address.PostCode,
            OrderDate = order.OrderInfo.OrderDate.ToString("dd.MM.yyyy"),
            OrderComment = order.OrderInfo.OrderComment,
            DeliveryType = EnumHelper<DeliveryType>.GetDisplayValue(order.OrderInfo.DeliveryType),
            PaymentType = EnumHelper<PaymentType>.GetDisplayValue(order.OrderInfo.PaymentType),
            Units = order.TotalItems,

            Items = order.OrderItems.Select(i => new OrderItemViewModel
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                FullPrice = i.FullPrice,
                DiscountedPrice = i.DiscountedPrice,
                Discount = i.Discount,
                Units = i.Units,
                AddInfo = i.AddInfo
            }).ToList(),
        };
    }
}