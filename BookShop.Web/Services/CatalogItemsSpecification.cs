using Ardalis.Specification;
using BookShop.DAL.Entities.Products;

namespace BookShop.Web.Services
{
    public class CatalogItemsSpecification : Specification<Book>
    {
        public CatalogItemsSpecification(params int[] ids)
        {
            Query.Where(c => ids.Contains(c.Id));
        }
    }
}