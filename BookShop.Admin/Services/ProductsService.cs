using BookShop.Admin.Interfaces;
using BookShop.Admin.Models;
using BookShop.Admin.Models.Product;

namespace BookShop.Admin.Services;

public class ProductService : BaseService, IProductService
{
    private readonly IHttpClientFactory clientFactory;

    public ProductService(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        this.clientFactory = clientFactory;
    }

    public async Task<T> GetBookById<T>(int id)
    {
        return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.GET,
            URL = SD.MainAPIBase + "/api/books?id=" + id,
            AccessToken = ""
        });
    }

    public async Task<T> GetBooksPaged<T>(int page, int pageSize)
    {
        return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.GET,
            URL = SD.MainAPIBase + "/api/books?page=" + page + "&pageSize=" + pageSize,
            AccessToken = ""
        });
    }


    public async Task<T> AddBook<T>(ProductDTO book)
    {
        return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.POST,
            URL = SD.MainAPIBase + "/api/books/add",
            Data = book,
            AccessToken = ""
        });
    }

    public async Task<T> UpdateBook<T>(ProductDTO book)
    {
         return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.POST,
            URL = SD.MainAPIBase + "/api/books/update",
            Data = book,
            AccessToken = ""
        });
    }

    public async Task<T> DeleteBook<T>(int id)
    {
        return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.DELETE,
            URL = SD.MainAPIBase + "/api/books/delete?id=" + id,
            AccessToken = ""
        });
    }

}