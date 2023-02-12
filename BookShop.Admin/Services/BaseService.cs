using BookShop.Admin.Interfaces;
using BookShop.Admin.Models;
using Newtonsoft.Json;

namespace BookShop.Admin.Services;

public class BaseService : IBaseService
{
    private readonly IHttpClientFactory httpClientFactory;

    public ResponseDTO responseModel { get; set; }

    public BaseService(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
        responseModel = new ResponseDTO();
    }



    public async Task<T> SendAsync<T>(APIRequest request)
    {
        try
        {
            var client = httpClientFactory.CreateClient("BookShopAdminAPI");
            HttpRequestMessage message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri(request.URL!);

            client.DefaultRequestHeaders.Clear();
            if (request.Data != null)
                message.Content = new StringContent(JsonConvert.SerializeObject(request.Data),
                                                    encoding: System.Text.Encoding.UTF8,
                                                    "application/json");

            switch (request.APIType)
            {
                case SD.APIType.GET:
                    message.Method = HttpMethod.Get;
                    break;
                case SD.APIType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case SD.APIType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                case SD.APIType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }

            HttpResponseMessage APIResponse = await client.SendAsync(message);

            var APIContent = await APIResponse.Content.ReadAsStringAsync();
            var APIResponseDTO = JsonConvert.DeserializeObject<T>(APIContent);
            return APIResponseDTO!;
        }
        catch (Exception e)
            {
                var dto = new ResponseDTO
                {
                    DisplayMessage = "Error",
                    ErrorMessage = new List<string> { Convert.ToString(e.Message) },
                    IsSuccess = false,
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponseDTO = JsonConvert.DeserializeObject<T>(res);
                return APIResponseDTO!;
            }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(true);
    }
}