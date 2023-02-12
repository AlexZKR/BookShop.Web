using static BookShop.Admin.SD;

namespace BookShop.Admin.Models;

    public class APIRequest
    {
        public APIType APIType { get; set; } = APIType.GET;
        public string? URL { get; set; }
        public object? Data { get; set; }
        public string? AccessToken { get; set; }
    }
