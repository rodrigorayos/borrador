using Store.Domain.Dtos.Store;
using Store.Domain.Models.Store;
using Store.Infrastructure.Database.EntityFramework.Entities.Store;

namespace Store.Infrastructure.Database.EntityFramework.Extensions.Store;

public static class AuthorizationExtension
{
    // Convierte un AuthorizationModel a AuthorizationEntity
    public static AuthorizationEntity ToEntity(this AuthorizationModel model)
    {
        if (model == null) return null;

        return new AuthorizationEntity
        {
            Id = model.Id,
            Date = model.Date,
            State = model.State,
            Description = model.Description
        };
    }

    // Convierte un AuthorizationDto a AuthorizationEntity
    public static AuthorizationEntity ToEntity(this AuthorizationDto dto)
    {
        if (dto == null) return null;

        return new AuthorizationEntity
        {
            Id = dto.Id,
            Date = dto.Date,
            State = dto.State,
            Description = dto.Description
        };
    }

    // Convierte un AuthorizationEntity a AuthorizationModel
    public static AuthorizationModel ToModel(this AuthorizationEntity entity)
    {
        if (entity == null) return null;

        return new AuthorizationModel(
            entity.Date,
            entity.State,
            entity.Description
        );
    }

    // Convierte un AuthorizationEntity a AuthorizationDto
    public static AuthorizationDto ToAuthorizationDto(this AuthorizationEntity entity)
    {
        if (entity == null) return null;

        return new AuthorizationDto(
            entity.Id,
            entity.Date, 
            entity.State, 
            entity.Description 
        );
    }

    // Convierte una lista de AuthorizationEntity a una lista de AuthorizationDto
    public static List<AuthorizationDto> ToAuthorizationDto(this IEnumerable<AuthorizationEntity> entities)
    {
        if (entities == null) return new List<AuthorizationDto>();

        return entities.Select(entity => entity.ToAuthorizationDto()).ToList();
    }
}