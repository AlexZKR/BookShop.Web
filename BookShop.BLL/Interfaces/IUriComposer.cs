namespace BookShop.BLL.Interfaces;

public interface IImageService
{
    Task<string> ComposePicUriById(int id);
}
