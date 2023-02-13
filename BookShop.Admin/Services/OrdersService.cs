using BookShop.Admin.Interfaces;
using BookShop.Admin.Models;

namespace BookShop.Admin.Services;

public class OrderService : BaseService, IOrderService
{
    private readonly IHttpClientFactory clientFactory;

    public OrderService(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        this.clientFactory = clientFactory;
    }
    public async Task<T> GetAllBuyersAsync<T>()
    {
        return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.GET,
            URL = SD.MainAPIBase + "/api/orders/buyers",
            AccessToken = ""
        });
    }

    public async Task<T> GetUserWithOrdersByIdAsync<T>(string id)
    {
       return await this.SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.GET,
                URL = SD.MainAPIBase + "/api/orders?id=" + id,
                AccessToken = ""
            });
    }
}