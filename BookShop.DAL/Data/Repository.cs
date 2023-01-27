using Ardalis.Specification.EntityFrameworkCore;
using BookShop.BLL.Interfaces;

namespace BookShop.DAL.Data;

public class Repository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public Repository(AppDbContext context) : base(context)
    {
    }
}