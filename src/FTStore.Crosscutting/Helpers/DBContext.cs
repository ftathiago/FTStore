using System;
using FTStore.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FTStore.Crosscutting.Helpers
{
    public static class DBContextExtension
    {
        public static IServiceCollection AddMemoryDataBase(this IServiceCollection services)
        {
            return services.AddDbContext<FTStoreDbContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .EnableSensitiveDataLogging()
            );
        }

        public static IServiceCollection AddMySQLDB(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<FTStoreDbContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseMySql(connectionString, m =>
                        m.MigrationsAssembly("FTStore.Infra")
                )
            );
            return services;
        }
    }
}
