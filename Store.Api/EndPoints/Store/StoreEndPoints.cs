using Store.Application.Services;
using Store.Application.Services.Store;
using Store.Domain.Dtos.Store;
using Store.Domain.Models.Store;

namespace Store.Api.EndPoints.Store;

public static class StoreEndPoints
{
    internal static void MapStoreEndPoints(this WebApplication webApp)
    {
        webApp.MapGroup("/stores").WithTags("Stores").MapGroupStores();
    }

    internal static void MapGroupStores(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost("", 
            async (StoreQueryDto dto, StoreService service) => await service.CreateStore(dto));

        groupBuilder.MapGet("", (StoreService service) => service.GetAllAsync());
        
        groupBuilder.MapGet("/get-by-id/{storeId:int}", 
            async (int storeId, StoreService service) => await service.GetByIdAsync(storeId));
        
        groupBuilder.MapGet("/get-by-name/{name}", 
            async (string name, StoreService service) => await service.GetByNameAsync(name));
        
        groupBuilder.MapPut("/{storeId:int}", 
            async (int storeId, StoreQueryDto updateDto, StoreService service) => 
            await service.UpdateAsync(storeId, updateDto));
        
        groupBuilder.MapDelete("/{storeId:int}", 
            async (int storeId, StoreService service) => await service.DeleteAsync(storeId));
    }
}