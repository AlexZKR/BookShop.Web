using AutoMapper;
using BookShop.Web.Models;
using BookShop.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookShop.Web.Models.Book;
using BookShop.Web.Infrastructure;
using BookShop.BLL.Entities.Products;

namespace BookShop.Web.Controllers;
[Route("api/books")]

public class ProductAPIController : ControllerBase
{
    private readonly IBookCatalogService bookService;
    private readonly IMapper mapper;
    private readonly IAppLogger<ProductAPIController> logger;
    protected ResponseDTO response;

    public ProductAPIController(IBookCatalogService bookService,
     IMapper mapper,
     IAppLogger<ProductAPIController> logger)
    {
        response = new ResponseDTO();
        this.bookService = bookService;
        this.mapper = mapper;
        this.logger = logger;
    }

    [HttpGet]
    [QueryParameterConstraint("id")]
    public async Task<ActionResult<object>> GetBook([FromQuery] int id)
    {
        try
        {
            var book = await bookService.GetBookAsync(id);
            var dto = mapper.Map<BookDTO>(book);
            response.Result = dto;
            logger.LogInformation($"Got book {id}");
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessage = new List<string> { ex.ToString() };
            logger.LogWarning($"GetBook {id} ex: {ex.Message}");
        }

        response.DisplayMessage = $"GetBook id: {id}";
        return response;
    }


    [HttpGet]
    [QueryParameterConstraint("page", "pageSize")]
    public async Task<ActionResult<object>> GetBooksPaged([FromQuery] int page, int pageSize)
    {
        try
        {
            var books = await bookService.GetBooksPaged(page, pageSize);
            var dtos = mapper.Map<List<BookDTO>>(books);
            response.Result = dtos;
            logger.LogWarning($"Action {nameof(GetBooksPaged)} page: {page}; pageSize: {pageSize}; Sent {dtos.Count} books");

        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessage = new List<string> { ex.ToString() };
            logger.LogWarning($"Action {nameof(GetBooksPaged)} page: {page}; pageSize: {pageSize}; ex: {ex.Message}");

        }
        response.DisplayMessage = $"Action {nameof(GetBooksPaged)} pageNo: {page}; pageSize {pageSize}";
        return response;
    }

    [HttpGet("count")]
    [ActionName("count")]
    public async Task<ActionResult<object>> CountAsync()
    {
         try
        {
            var booksCount = await bookService.CountBooksAsync();
            // var dto = mapper.Map<CountDataDTO>(booksCount);
            response.Result = new CountDataDTO {BooksTotal = booksCount};
            logger.LogInformation($"Action {nameof(CountAsync)} Got {booksCount} books in DB");
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessage = new List<string> { ex.ToString() };
            logger.LogInformation($"Action {nameof(CountAsync)} Som problem");
        }
        return response;
    }

    [HttpPost("add")]
    public async Task<ActionResult<object>> AddBookAsync([FromBody] BookDTO bookDTO)
    {
        try
        {
            logger.LogInformation($"Received {bookDTO.Name}, {bookDTO.Description}");
            var model = mapper.Map<Book>(bookDTO);
            var book = await bookService.AddBookAsync(model);
            var dtoResponse = mapper.Map<BookDTO>(book);
            response.Result = dtoResponse;
            logger.LogWarning($"Action {nameof(AddBookAsync)} added book id: {book.Id}");
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessage = new List<string> { ex.ToString() };
            logger.LogWarning($"Action {nameof(AddBookAsync)} ex: {ex.Message}");

        }
        response.DisplayMessage = $"Action {nameof(AddBookAsync)}";
        return response;
    }


    [HttpPost("update")]
    public async Task<ActionResult<object>> UpdateBookAsync([FromBody] BookDTO bookDTO)
    {
        try
        {
            logger.LogInformation($"Received {bookDTO.Name}, {bookDTO.Description}, id {bookDTO.Id} for updating");
            var model = mapper.Map<Book>(bookDTO);
            var book = await bookService.UpdateBookAsync(model);
            var dtoResponse = mapper.Map<BookDTO>(book);
            response.Result = dtoResponse;
            logger.LogWarning($"Action {nameof(UpdateBookAsync)} updated book id: {book.Id}");
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessage = new List<string> { ex.ToString() };
            logger.LogWarning($"Action {nameof(UpdateBookAsync)} ex: {ex.Message}");
        }
        response.DisplayMessage = $"Action {nameof(UpdateBookAsync)}";
        return response;
    }
    [HttpDelete("delete")]
    [QueryParameterConstraint("id")]
    public ActionResult<object> DeleteBookAsync([FromQuery] int id)
    {
        try
        {
            logger.LogInformation($"Received book id {id} to delete");
            bookService.DeleteBookAsync(id);
            logger.LogWarning($"Action {nameof(DeleteBookAsync)} deleted book id: {id}");
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessage = new List<string> { ex.ToString() };
            logger.LogWarning($"Action {nameof(DeleteBookAsync)} ex: {ex.Message}");
        }
        response.DisplayMessage = $"Action {nameof(DeleteBookAsync)}";
        return response;
    }
}