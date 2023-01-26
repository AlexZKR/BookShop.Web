// using Ardalis.Specification;
// using Microsoft.eShopWeb.ApplicationCore.Entities;

// namespace BookShop.Web.Specifications;

// public class CatalogFilterSpecification : Specification<CatalogItem>
// {
//     public CatalogFilterSpecification(int? brandId, int? typeId)
//     {
//         Query.Where(i => (!brandId.HasValue || i.CatalogBrandId == brandId) &&
//             (!typeId.HasValue || i.CatalogTypeId == typeId));
//     }
// // }
