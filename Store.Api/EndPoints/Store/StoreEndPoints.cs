using Store.Application.Services;
using Store.Application.Services.Store;
using Store.Domain.Dtos.Store;
using Store.Domain.Models.Store;

namespace Store.Api.EndPoints.Store;

public static class StoreEndPoints
{
    internal static void MapStoreEndPoints(this WebApplication webApp)
    {
        // Define un grupo de endpoints para las tiendas
        webApp.MapGroup("/stores").WithTags("Stores").MapGroupStores();
    }

    internal static void MapGroupStores(this RouteGroupBuilder groupBuilder)
    {
        // Crear una nueva tienda
        groupBuilder.MapPost("", async (StoreQueryDto dto, StoreService service) => await service.CreateStore(dto));

        // Obtener todas las tiendas
        groupBuilder.MapGet("", (StoreService service) => service.GetAllAsync());

        // Obtener una tienda por su ID
        groupBuilder.MapGet("/get-by-id/{storeId:int}", async (int storeId, StoreService service) => await service.GetByIdAsync(storeId));

        // Obtener una tienda por su nombre
        groupBuilder.MapGet("/get-by-name/{name}", async (string name, StoreService service) => await service.GetByNameAsync(name));

        // Actualizar una tienda
        groupBuilder.MapPut("/{storeId:int}", async (int storeId, StoreQueryDto updateDto, StoreService service) => 
            await service.UpdateAsync(storeId, updateDto));

        // Eliminar una tienda
        groupBuilder.MapDelete("/{storeId:int}", async (int storeId, StoreService service) => await service.DeleteAsync(storeId));
    }
}