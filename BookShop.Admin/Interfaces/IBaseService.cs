using BookShop.Admin.Models;

namespace BookShop.Admin.Interfaces;

public interface IBaseService : IDisposable
{
    ResponseDTO responseModel { get; set; }
    Task<T> SendAsync<T>(APIRequest request);
}