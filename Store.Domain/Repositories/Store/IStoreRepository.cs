using Store.Domain.Dtos.Store;
using Store.Domain.Repositories.Common;

namespace Store.Domain.Repositories.Store;

public interface IStoreRepository : IGenericRepository<StoreDto>
{
    Task<List<StoreDto>> GetAllAsync(); 
    Task<IEnumerable<StoreDto>> SearchByNameAsync(string searchTerm);
}