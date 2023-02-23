using AutoMapper;
using BookShop.Admin.Models.Order;
using BookShop.Admin.Models.Product;
using BookShop.Admin.ViewModels.Catalog;
using BookShop.Admin.ViewModels.Order;

namespace BookShop.Admin.Configuration.MapperConfig;

public class DTOMapProfile : Profile
{
    public DTOMapProfile()
    {
        CreateMap<OrderDTO, OrderViewModel>().ReverseMap();
        CreateMap<OrderItemDTO, OrderItemViewModel>().ReverseMap();
        CreateMap<BuyerDTO, BuyerViewModel>().ReverseMap();
        CreateMap<ProductDTO, ProductViewModel>().ReverseMap();
    }
}