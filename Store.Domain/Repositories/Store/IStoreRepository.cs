using Store.Domain.Dtos;
using Store.Domain.Repositories.Common;

namespace Store.Domain.Repositories.Store;

public interface IStoreRepository : IGenericRepository<StoreDto>
{
    Task<bool> IsStoreNameUniqueAsync(string storeName);
}