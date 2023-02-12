namespace BookShop.Admin.Interfaces;

public interface IEntityService : IBaseService
{
    Task<T> GetAllEntitiesAsync<T>();
    Task<T> GetEntityByIdAsync<T>(int id);


}