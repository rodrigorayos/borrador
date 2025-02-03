using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Database.EntityFramework.Entities;
using Store.Infrastructure.Database.EntityFramework.Entities.Store;

namespace Store.Infrastructure.Database.EntityFramework.Context
{
    public class StoreDbContext : DbContext
    {
        public DbSet<StoreEntity> Stores { get; set; }
        
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) {}
        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }
        
        private void UpdateAuditFields()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.CreatedBy = GetCurrentUserId();
                        entry.Entity.LastModifiedByAt = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = GetCurrentUserId();
                        break;

                    case EntityState.Modified:
                        entry.Property(nameof(BaseEntity.CreatedAt)).IsModified = false;
                        entry.Property(nameof(BaseEntity.CreatedBy)).IsModified = false;
                        entry.Entity.LastModifiedByAt = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = GetCurrentUserId();
                        break;
                }
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ejemplo de configuración de relaciones (ajusta según tus necesidades)
            modelBuilder.Entity<StoreEntity>()
                .HasKey(s => s.Id); // Define la clave primaria

            // Agrega más configuraciones de relaciones aquí si es necesario
        }

        // Método para obtener el ID del usuario actual (simulado)
        private int GetCurrentUserId()
        {
            // Aquí puedes implementar la lógica para obtener el ID del usuario actual
            // Por ahora, devolvemos un valor simulado
            return 123;
        }
    }
}