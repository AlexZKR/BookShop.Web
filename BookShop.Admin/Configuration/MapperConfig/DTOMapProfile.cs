using AutoMapper;
using BookShop.Admin.Models.Order;
using BookShop.Admin.ViewModels.Order;

namespace BookShop.Admin.Configuration.MapperConfig;

public class DTOMapProfile : Profile
{
    public DTOMapProfile()
    {
        CreateMap<OrderDTO, OrderViewModel>().ReverseMap();
        //.ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType));

        CreateMap<OrderItemDTO, OrderItemViewModel>().ReverseMap();

        CreateMap<BuyerDTO, BuyerViewModel>().ReverseMap();
    }
}