using Microsoft.EntityFrameworkCore;
using Store.Domain.Dtos.Store;
using Store.Domain.Repositories.Store;
using Store.Infrastructure.Database.EntityFramework.Context;
using Store.Infrastructure.Database.EntityFramework.Entities.Store;
using Store.Infrastructure.Database.EntityFramework.Extensions.Store;
using Store.Infrastructure.Database.EntityFramework.Repositories.Common;

namespace Store.Infrastructure.Database.EntityFramework.Repositories.Store
{
    public class StoreRepository : GenericRepository<StoreEntity>, IStoreRepository<StoreEntity>
    {
        private readonly StoreDbContext _context;

        public StoreRepository(StoreDbContext context) : base(context)
        {
            _context = context;
        }

        // ✅ Obtener todas las tiendas
        public async Task<List<StoreDto>> GetAllAsync()
        {
            var entities = await _context.Stores.ToListAsync();
            return entities.Select(s => s.ToStoreDto()).ToList(); // Convertimos la lista de entidades a DTOs
        }

        // ✅ Crear una nueva tienda
        public async Task<StoreDto> CreateAsync(StoreDto dto)
        {
            var entity = dto.ToEntity(); // Convertir DTO a entidad
            var createdEntity = await base.CreateAsync(entity);
            return createdEntity.ToStoreDto(); // Convertir entidad a DTO
        }

        // ✅ Obtener una tienda por su ID
        public new async Task<StoreDto?> GetByIdAsync(int id)
        {
            var entity = await _context.Stores
                .FirstOrDefaultAsync(s => s.Id == id);

            return entity?.ToStoreDto(); // Convertir entidad a DTO
        }

        // ✅ Obtener una tienda por su nombre
        public async Task<IEnumerable<StoreDto>> SearchByNameAsync(string searchTerm)
        {
            return await _context.Stores
                .Where(s => s.Name.ToLower().Contains(searchTerm.ToLower()))
                .Select(s => s.ToStoreDto()) // Convertimos a DTO
                .ToListAsync();
        }

        // ✅ Actualizar una tienda
        public async Task<StoreDto> UpdateAsync(StoreDto dto)
        {
            var entity = await _context.Stores.FindAsync(dto.Id);
            if (entity == null)
            {
                throw new KeyNotFoundException("Store not found");
            }

            entity.Name = dto.Name;
            entity.Ubication = dto.Ubication;
            entity.Capacity = dto.Capacity;

            _context.Stores.Update(entity);
            await _context.SaveChangesAsync();

            return entity.ToStoreDto(); // Convertir entidad a DTO
        }

        // ✅ Eliminar una tienda por su ID
        public new async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Stores.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            _context.Stores.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
