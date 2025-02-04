using Store.Application.Services.Store;
using Store.Domain.Dtos.Store;

namespace Store.Api.EndPoints.Store;

public static class AuthorizationEndPoints
{
    internal static void MapAuthorizationEndPoints(this WebApplication webApp)
    {
        webApp.MapGroup("/authorizations").WithTags("Authorizations").MapGroupAuthorizations();
    }

    internal static void MapGroupAuthorizations(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost("",
            async (AuthorizationQueryDto dto, AuthorizationService service) => await service.CreateAuthorization(dto));
        
        groupBuilder.MapGet("", (AuthorizationService service) => service.GetAllAsync());
        
        groupBuilder.MapGet("/get-by-id/{authorizationId:int}",
            async (int authorizationId, AuthorizationService service) => await service.GetByIdAsync(authorizationId));
        
        groupBuilder.MapPut("/{authorizationId:int}",
            async (int authorizationId, AuthorizationQueryDto updateDto, AuthorizationService service) =>
            await service.UpdateAsync(authorizationId, updateDto));

        groupBuilder.MapDelete("/{authorizationId:int}",
            async (int authorizationId, AuthorizationService service) => await service.DeleteAsync(authorizationId));
    }
}