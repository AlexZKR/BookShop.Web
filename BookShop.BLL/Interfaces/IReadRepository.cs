using Ardalis.Specification;

namespace BookShop.BLL.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
