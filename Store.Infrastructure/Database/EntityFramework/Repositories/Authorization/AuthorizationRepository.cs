using Microsoft.EntityFrameworkCore;
using Store.Domain.Dtos.Authorization;
using Store.Domain.Repositories.Authorization;
using Store.Infrastructure.Database.EntityFramework.Context;
using Store.Infrastructure.Database.EntityFramework.Entities.Authorization;
using Store.Infrastructure.Database.EntityFramework.Extensions.Authorization;
using Store.Infrastructure.Database.EntityFramework.Repositories.Common;

namespace Store.Infrastructure.Database.EntityFramework.Repositories.Authorization
{
    public class AuthorizationRepository : GenericRepository<AuthorizationEntity>, IAuthorizationRepository
    {
        private readonly StoreDbContext _context;

        public AuthorizationRepository(StoreDbContext context) : base(context)
        {
            _context = context;
        }

        // ✅ Obtener todas las autorizaciones
        public async Task<List<AuthorizationDto>> GetAllAsync()
        {
            var entities = await _context.Authorizations.ToListAsync();
            return entities.ToAuthorizationDto(); // Convertir entidades a DTOs
        }

        // ✅ Crear una nueva autorización
        public async Task<AuthorizationDto> CreateAsync(AuthorizationDto dto)
        {
            var entity = dto.ToEntity(); // Convertir DTO a entidad
            var createdEntity = await base.CreateAsync(entity);
            return createdEntity.ToAuthorizationDto(); // Convertir entidad a DTO
        }

        // ✅ Obtener una autorización por su ID
        public new async Task<AuthorizationDto?> GetByIdAsync(int id)
        {
            var entity = await _context.Authorizations
                .FirstOrDefaultAsync(a => a.Id == id);

            return entity?.ToAuthorizationDto(); // Convertir entidad a DTO
        }

        // ✅ Actualizar una autorización
        public async Task<AuthorizationDto> UpdateAsync(AuthorizationDto dto)
        {
            var entity = await _context.Authorizations.FindAsync(dto.Id);
            if (entity == null)
            {
                throw new KeyNotFoundException("Authorization not found");
            }

            entity.Date = dto.Date;
            entity.State = dto.State;
            entity.Description = dto.Description;

            _context.Authorizations.Update(entity);
            await _context.SaveChangesAsync();

            return entity.ToAuthorizationDto(); // Convertir entidad a DTO
        }

        // ✅ Eliminar una autorización por su ID
        public new async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Authorizations.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            _context.Authorizations.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}