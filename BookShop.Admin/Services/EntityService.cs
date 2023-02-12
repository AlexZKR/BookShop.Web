using BookShop.Admin.Interfaces;
using BookShop.Admin.Models;

namespace BookShop.Admin.Services;

public class EntityService : BaseService, IEntityService
{
    private readonly IHttpClientFactory clientFactory;

    public EntityService(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        this.clientFactory = clientFactory;
    }
    public async Task<T> GetAllEntitiesAsync<T>()
    {
        return await SendAsync<T>(new APIRequest()
        {
            APIType = SD.APIType.GET,
            URL = SD.MainAPIBase + "/api/orders",
            AccessToken = ""
        });
    }

    public async Task<T> GetEntityByIdAsync<T>(int id)
    {
       return await this.SendAsync<T>(new APIRequest()
            {
                APIType = SD.APIType.GET,
                URL = SD.MainAPIBase + "/api/orders/" + id,
                AccessToken = ""
            });
    }
}