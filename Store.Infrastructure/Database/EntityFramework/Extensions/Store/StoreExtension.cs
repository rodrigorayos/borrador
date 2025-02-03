using Store.Domain.Models.Store;
using Store.Domain.Dtos.Store;
using Store.Infrastructure.Database.EntityFramework.Entities.Store;

namespace Store.Infrastructure.Database.EntityFramework.Extensions.Store;

public static class StoreExtension
{
    // Convierte un StoreModel a StoreEntity
    public static StoreEntity ToEntity(this StoreModel model)
    {
        if (model == null) return null;

        return new StoreEntity
        {
            Id = model.Id,
            Name = model.Name,
            Ubication = model.Ubication,
            Capacity = model.Capacity
        };
    }

    // Convierte un StoreDto a StoreEntity
    public static StoreEntity ToEntity(this StoreDto dto)
    {
        if (dto == null) return null;

        return new StoreEntity
        {
            Id = dto.Id,
            Name = dto.Name,
            Ubication = dto.Ubication,
            Capacity = dto.Capacity
        };
    }

    // Convierte un StoreEntity a StoreModel
    public static StoreModel ToModel(this StoreEntity entity)
    {
        if (entity == null) return null;

        return new StoreModel(
            entity.Name,       
            entity.Ubication,   
            entity.Capacity     
        );
    }

    // Convierte un StoreEntity a StoreDto
    public static StoreDto ToStoreDto(this StoreEntity entity)
    {
        if (entity == null) return null;

        return new StoreDto(
            entity.Id,          
            entity.Name,        
            entity.Ubication,   
            entity.Capacity   
        );
    }

    // Convierte una lista de StoreEntity a una lista de StoreDto
    public static List<StoreDto> ToStoreDto(this IEnumerable<StoreEntity> entities)
    {
        if (entities == null) return new List<StoreDto>();
        
        return entities.Select(entity => entity.ToStoreDto()).ToList();
    }
}