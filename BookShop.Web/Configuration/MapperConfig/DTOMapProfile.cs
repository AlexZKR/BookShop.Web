using AutoMapper;
using BookShop.BLL.Entities.Order;
using BookShop.Web.Models.DTOs.Order;

namespace BookShop.Web.Configuration.MapperConfig;

public class DTOMapProfile : Profile
{
    public DTOMapProfile()
    {
        CreateMap<OrderDTO, Order>().ReverseMap()
            .ForMember(dest => dest.BuyerId, opt => opt.MapFrom(src => src.Buyer.BuyerId))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderInfo.OrderDate));

        CreateMap<OrderItemDTO, OrderItem>().ReverseMap();

        CreateMap<BuyerDTO, Buyer>().ReverseMap()
        .ForMember(dest => dest.Id, opt=>opt.MapFrom(src => src.BuyerId))
        .ForMember(dest => dest.Name, opt=>opt.MapFrom(src => (src.FirstName + " " + src.LastName)));
    }
}