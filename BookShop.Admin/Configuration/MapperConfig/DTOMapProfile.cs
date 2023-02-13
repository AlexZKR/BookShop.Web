using AutoMapper;
using BookShop.Admin.Models.Order;
using BookShop.Admin.ViewModels.Order;

namespace BookShop.Web.Configuration.MapperConfig;

public class DTOMapProfile : Profile
{
    public DTOMapProfile()
    {
        CreateMap<OrderDTO, OrderViewModel>().ReverseMap();

        CreateMap<OrderItemDTO, OrderItemViewModel>().ReverseMap();

        CreateMap<BuyerDTO, BuyerViewModel>().ReverseMap();
    }
}