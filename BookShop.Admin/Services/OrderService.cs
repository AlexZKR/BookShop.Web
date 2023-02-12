using BookShop.Admin.Interfaces;
using BookShop.Admin.Models;
using Newtonsoft.Json;

namespace BookShop.Admin.Services;

public class OrderService : BaseService, IOrderService
{
    private readonly IHttpClientFactory clientFactory;

    public OrderService(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        this.clientFactory = clientFactory;
    }
    public async Task<T> GetAllOrdersAsync<T>()
    {
        return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.GET,
            URL = SD.AdminAPIBase + "/api/orders",
            AccessToken = ""
        });
    }

    public async Task<T> GetOrderByIdAsync<T>(int id)
    {
       return await this.SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.GET,
                URL = SD.AdminAPIBase + "/api/orders/" + id,
                AccessToken = ""
            });
    }
}