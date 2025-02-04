using Store.Domain.Dtos.Store;
using Store.Domain.Repositories.Common;

namespace Store.Domain.Repositories.Store;

public interface IAuthorizationRepository : IGenericRepository<AuthorizationDto>
{
    Task<List<AuthorizationDto>> GetAllAsync(); 
}