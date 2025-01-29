namespace Store.Domain.Repositories.Common;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<T>> GetAllAsync<T>() where T : class;
    Task<T> GetByIdAsync<T>(int id) where T : class;
    Task<T> AddAsync<T>(T entity) where T : class;
    Task<T> UpdateAsync<T>(T entity) where T : class;
    Task<T> DeleteAsync<T>(int id) where T : class;
}
