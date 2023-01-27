using Ardalis.Specification;

namespace BookShop.Web.Services.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
{
}
