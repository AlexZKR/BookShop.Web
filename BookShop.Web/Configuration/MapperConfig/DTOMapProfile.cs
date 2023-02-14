using AutoMapper;
using BookShop.BLL.Entities.Enums;
using BookShop.BLL.Entities.Order;
using BookShop.Web.Infrastructure;
using BookShop.Web.Models.DTOs.Order;

namespace BookShop.Web.Configuration.MapperConfig;

public class DTOMapProfile : Profile
{
    public DTOMapProfile()
    {
        CreateMap<OrderDTO, Order>().ReverseMap()
            .ForMember(dest => dest.BuyerId, opt => opt.MapFrom(src => src.Buyer.BuyerId))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderInfo.OrderDate))
            .ForMember(dest => dest.BuyerName, opt => opt.MapFrom(src => (src.Buyer.FirstName + " " + src.Buyer.LastName)))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Buyer.PhoneNumber))
            .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => (EnumHelper<PaymentType>.GetName(src.OrderInfo.PaymentType))))
            .ForMember(dest => dest.DeliveryType, opt => opt.MapFrom(src => (EnumHelper<DeliveryType>.GetName(src.OrderInfo.DeliveryType))))
            .ForMember(dest => dest.OrderComment, opt => opt.MapFrom(src => src.OrderInfo.OrderComment))
            .ForMember(dest => dest.IsInProcess, opt => opt.MapFrom(src => src.IsInProcess));

        CreateMap<OrderItemDTO, OrderItem>().ReverseMap();

        CreateMap<BuyerDTO, Buyer>().ReverseMap()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BuyerId))
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => (src.FirstName + " " + src.LastName)));
    }
}