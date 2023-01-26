using Ardalis.Specification;

namespace BookShop.Web.Services.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{
}
