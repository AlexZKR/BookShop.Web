using Ardalis.Specification;
using BookShop.BLL.Entities.Enums;
using BookShop.BLL.Entities.Products;

namespace BookShop.BLL.Specifications.CatalogSpecifications;

public class BookCatalogGetNumberOfTopSoldItemsSpecification : Specification<Book>
{
    public BookCatalogGetNumberOfTopSoldItemsSpecification(int quantity)
    {
        Query.OrderByDescending(p => p.Sold).Take(quantity);
    }
}
