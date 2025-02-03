namespace Store.Domain.Repositories.Common
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity); // Create
        Task<TEntity?> GetByIdAsync(int id); // Read (by ID)
        Task<bool> DeleteAsync(int id); // Delete
        Task<TEntity> UpdateAsync(TEntity entity); // Update
    }
}