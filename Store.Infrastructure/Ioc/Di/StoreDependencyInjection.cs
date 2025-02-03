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
using Store.Application.Services.Authorization;
using Store.Application.Services.Store;
using Store.Application.Validators.Authorization;
using Store.Application.Validators.Store;
using Store.Domain.Models.Authorization;
using Store.Domain.Repositories.Authorization;
using Store.Infrastructure.Database.EntityFramework.Entities.Authorization;
using Store.Infrastructure.Database.EntityFramework.Entities.Store;
using Store.Infrastructure.Database.EntityFramework.Repositories.Authorization;

namespace Store.Infrastructure.Ioc.Di
{
    public static class StoreDependencyInjection
    {
        public static IServiceCollection RegisterDataBase(this IServiceCollection collection, 
            IConfiguration configuration)
        {
            string connectionString = configuration["ConnectionStrings:remoteConnection"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), 
                    "La cadena de conexión no puede ser nula ni estar vacía.");
            }

            collection.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return collection;
        }
        
        public static IServiceCollection RegisterServices(this IServiceCollection collection, 
            IConfiguration configuration)
        {
            collection.AddTransient<StoreService>();
            collection.AddTransient<AuthorizationService>();
            /*-------------------------------------------------------------------------*/
            collection.AddScoped<IGenericRepository<StoreEntity>, GenericRepository<StoreEntity>>();
            collection.AddScoped<IGenericRepository<AuthorizationEntity>, GenericRepository<AuthorizationEntity>>();
            /*--------------------------------------------------------------------------*/
            collection.AddTransient<IStoreRepository, StoreRepository>();
            collection.AddTransient<IAuthorizationRepository, AuthorizationRepository>();
            /*--------------------------------------------------------------------------*/
            collection.AddTransient<IValidator<StoreModel>, StoreValidation>();
            collection.AddTransient < IValidator<AuthorizationModel>, AuthorizationValidation>();

            return collection;
        }
        
        public static IServiceCollection RegisterRepositories(this IServiceCollection collection)
        {
            collection.AddTransient<IStoreRepository, StoreRepository>();
            collection.AddTransient<IAuthorizationRepository, AuthorizationRepository>();

            return collection;
        }
        
        public static IServiceCollection RegisterProviders(this IServiceCollection collection)
        {
            return collection;
        }
    }
}
