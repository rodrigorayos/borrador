using Store.Domain.Dtos.Authorization;
using Store.Domain.Dtos.Store;
using Store.Domain.Repositories.Common;

namespace Store.Domain.Repositories.Authorization;

public interface IAuthorizationRepository : IGenericRepository<AuthorizationDto>
{
    Task<List<AuthorizationDto>> GetAllAsync(); 
}