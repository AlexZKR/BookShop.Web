using Ardalis.Specification.EntityFrameworkCore;

namespace BookShop.DAL.Data;

public class Repository : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class