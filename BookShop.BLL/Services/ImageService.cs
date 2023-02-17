
using BookShop.BLL.Entities.Products;
using BookShop.BLL.Exceptions;
using BookShop.BLL.Interfaces;
using BookShop.BLL.Specifications.CatalogSpecifications;

namespace BookShop.BLL.Services;

public class ImageService : IImageService
{
    private readonly IRepository<BaseProduct> productRepository;

    public ImageService(IRepository<BaseProduct> productRepository)
    {
        this.productRepository = productRepository;
    }

        public async Task<string> ComposePicUriById(int id)
    {
        var product = await productRepository.FirstOrDefaultAsync(new BaseProductSpecification(id));
        if(product == null) throw new NotFoundException($"Product with id {id} was not found");
        return product.PictureUri;
    }
}
