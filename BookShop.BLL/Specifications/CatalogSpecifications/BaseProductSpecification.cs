using Ardalis.Specification;
using BookShop.BLL.Entities.Products;

namespace BookShop.BLL.Specifications.CatalogSpecifications;
public class BaseProductSpecification : Specification<BaseProduct>
{
    public BaseProductSpecification(int id)
    {
        Query.Where(b => b.Id == id);
    }
    public BaseProductSpecification(BaseProduct entity)
    {
        Query.Where(b => b.Id == entity.Id);
    }
}