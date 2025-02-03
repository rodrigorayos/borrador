namespace Store.Domain.Repositories.Common
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}