using AutoMapper;
using BookShop.Admin.Interfaces;
using BookShop.Admin.Models;
using BookShop.Admin.Models.Product;
using BookShop.Admin.ViewModels.Catalog;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookShop.Admin.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService productService;
    private readonly IMapper mapper;

    public ProductsController(IProductService productService,
    IMapper mapper)
    {
        this.productService = productService;
        this.mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var vm = new ProductsPageViewModel();

        var response = await productService.GetBooksPaged<ResponseDTO>(0, SD.PageSize);
        if (response != null && response.IsSuccess)
        {
            var list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result)!);
            vm.Products = list!.Select(d => mapper.Map<ProductViewModel>(d)).OrderByDescending(x => x.Name).ToList();
            vm.Pagination.ActualPage = 1;
            vm.Pagination.ItemsPerPage = SD.PageSize;
        }
        else
        {
            vm.StatusMessage = "Error loading data. Try later.";
            vm.IsSuccess = false;
            return View(vm);
        }

        if (vm.Products.Count == 0) vm.StatusMessage = "Nothing found";

        return View(vm);
    }
}