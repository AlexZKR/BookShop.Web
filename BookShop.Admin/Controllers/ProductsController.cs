using AutoMapper;
using BookShop.Admin.Extensions;
using BookShop.Admin.Infrastructure;
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

    [HttpGet("GetProductsPage")]
    public async Task<ActionResult<object>> GetProductsPage([FromQuery]int page)
    {
        var response = await productService.GetBooksPaged<ResponseDTO>(page, SD.PageSize);
        if (response != null && response.IsSuccess)
        {
            var list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result)!);
            return PartialView("_ProductListPartial", list!.Select(d => mapper.Map<ProductViewModel>(d)).OrderByDescending(x => x.Name).ToList());
        }
        else
        {
            return NotFound();
        }
    }

    public async Task<ActionResult<object>> GetCountData()
    {
        var response = await productService.CountBooks<ResponseDTO>();
        if (response != null && response.IsSuccess)
        {
            return Json(new {
                count = (mapper.Map<CountDataDTO>(response.Result)).BooksTotal,
                pageSize = SD.PageSize
            });
        }
        else
        {
            return NotFound();
        }
    }


    [HttpGet("BookInfo")]
    public async Task<IActionResult> BookInfo([FromQuery] int itemId)
    {
        var vm = new ProductViewModel();

        var response = await productService.GetBookById<ResponseDTO>(itemId);
        if (response != null && response.IsSuccess)
        {
            var book = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result)!);
            vm = mapper.Map<ProductViewModel>(book);
        }
        else
        {
            vm.StatusMessage = "Error loading data. Try later.";
            vm.IsSuccess = false;
            return View(vm);
        }

        if (this.Request.IsAjaxRequest())
            return PartialView("_ProductInfoPartial", vm);
        else
            return View("_ProductInfoPartial", vm);
    }


    [HttpDelete]
    [QueryParameterConstraintAttribute("itemId")]
    public async Task<ActionResult<object>> BookDelete([FromQuery] int itemId)
    {
        var response = await productService.DeleteBook<ResponseDTO>(itemId);
        if (response != null && response.IsSuccess)
        {
            if (Request.IsAjaxRequest())
            {
                return Json(new
                {
                    Success = response.IsSuccess,
                    Id = itemId
                });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
        else
        {
            if (Request.IsAjaxRequest())
            {
                return Json(new
                {
                    success = false,
                });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var vm = new ProductViewModel();

        var response = await productService.GetBookById<ResponseDTO>(id);
        if (response != null && response.IsSuccess)
        {
            var book = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result)!);
            vm = mapper.Map<ProductViewModel>(book);
        }
        else
        {
            vm.StatusMessage = "Error loading data. Try later.";
            vm.IsSuccess = false;
            return View(vm);
        }
            return View("ProductEdit", vm);
    }

    [HttpPost("BookEdit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BookEdit(ProductViewModel model)
    {
        if (ModelState.IsValid)
        {
            var dto = mapper.Map<ProductDTO>(model);
            await productService.UpdateBook<ResponseDTO>(dto);
            return RedirectToAction(nameof(Index));
        }
        return View("ProductEdit",model);
    }

}