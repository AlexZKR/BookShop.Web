using AutoMapper;
using BookShop.BLL.Entities.Basket;
using BookShop.BLL.Interfaces;
using BookShop.Web.Models.Basket;

namespace BookShop.Web.Configuration.MapperConfig;

public class ViewModelMapProfile : Profile
{
    public ViewModelMapProfile()
    {
        CreateMap<BasketItem, BasketItemViewModel>().ReverseMap();
    }
}