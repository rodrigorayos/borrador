using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Database.EntityFramework.Common;
using Store.Infrastructure.Database.EntityFramework.Entities.Authorization;
using Store.Infrastructure.Database.EntityFramework.Entities.Store;

namespace Store.Infrastructure.Database.EntityFramework.Context
{
    public class StoreDbContext : DbContext
    {
        public DbSet<StoreEntity> Stores { get; set; }
        public DbSet<AuthorizationEntity> Authorizations { get; set; }

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

            modelBuilder.Entity<StoreEntity>().HasKey(xS => xS.Id);
            modelBuilder.Entity<AuthorizationEntity>().HasKey(xA => xA.Id);
        }


        private int GetCurrentUserId()
        {
            return 123; // Simulación de un usuario autenticado
        }
    }
}
