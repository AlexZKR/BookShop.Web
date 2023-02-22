using AutoMapper;
using BookShop.BLL.Entities.BasketAggregate;
using BookShop.BLL.Entities.Products;
using BookShop.Web.Models.Basket;
using BookShop.Web.Models.Catalog;

namespace BookShop.Web.Configuration.MapperConfig;

public class ViewModelMapProfile : Profile
{
    public ViewModelMapProfile()
    {
        CreateMap<BasketItem, BasketItemViewModel>().ReverseMap();
        CreateMap<CatalogItemViewModel, Book>().ReverseMap()
        .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
        .ForPath(dest => dest.Rating.Rating, opt => opt.MapFrom(src => src.Rating));
    }
}