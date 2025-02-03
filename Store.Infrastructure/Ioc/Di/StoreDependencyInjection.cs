using Store.Domain.Models.Store;
using Store.Domain.Repositories.Store;
using Store.Domain.Repositories.Common;
using Store.Infrastructure.Database.EntityFramework.Context;
using Store.Infrastructure.Database.EntityFramework.Repositories.Store;
using Store.Infrastructure.Database.EntityFramework.Repositories.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Application.Services.Store;
using Store.Application.Validators.Store;
using Store.Domain.Dtos.Store;
using Store.Infrastructure.Database.EntityFramework.Entities.Store;

namespace Store.Infrastructure.Ioc.Di
{
    public static class StoreDependencyInjection
    {
        public static IServiceCollection RegisterDataBase(this IServiceCollection collection, IConfiguration configuration)
        {
            string connectionString = configuration["ConnectionStrings:remoteConnection"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "La cadena de conexión no puede ser nula ni estar vacía.");
            }

            collection.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return collection;
        }
        
        public static IServiceCollection RegisterServices(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddTransient<StoreService>();
            
            collection.AddScoped<IGenericRepository<StoreEntity>, GenericRepository<StoreEntity>>();
            
            collection.AddTransient<IStoreRepository<StoreEntity>, StoreRepository>();
            
            collection.AddTransient<IValidator<StoreModel>, StoreValidation>();

            return collection;
        }
        
        public static IServiceCollection RegisterRepositories(this IServiceCollection collection)
        {
            collection.AddTransient<IStoreRepository<StoreEntity>, StoreRepository>();

            return collection;
        }
        
        public static IServiceCollection RegisterProviders(this IServiceCollection collection)
        {
            return collection;
        }
    }
}
