using Store.Domain.Dtos.Store;
using Store.Domain.Repositories.Common;

namespace Store.Domain.Repositories.Store;

public interface IStoreRepository<TEntity> : IGenericRepository<StoreDto>
{
    Task<List<TEntity>> GetAllAsync(); // Read (all)
    Task<IEnumerable<StoreDto>> SearchByNameAsync(string searchTerm); 
}