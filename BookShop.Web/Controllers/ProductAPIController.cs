using AutoMapper;
using BookShop.Web.Models;
using BookShop.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookShop.Web.Models.Book;

namespace BookShop.Web.Controllers;
[Route("api/books")]

public class ProductAPIController : ControllerBase
{
    private readonly IBookCatalogService bookService;
    private readonly IMapper mapper;
    protected ResponseDTO response;

    public ProductAPIController(IBookCatalogService bookService, IMapper mapper)
    {
       response = new ResponseDTO();
        this.bookService = bookService;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<object>> GetBook([FromQuery] int id)
    {
        try
        {
            var book = await bookService.GetBookAsync(id);
            var dto = mapper.Map<BookDTO>(book);
            response.Result = dto;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessage = new List<string> { ex.ToString() };
        }

        response.DisplayMessage =$"GetBook id: {id}";
        return response;
    }

}
