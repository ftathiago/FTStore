using Microsoft.Extensions.DependencyInjection;
using FTStore.Domain.Repository;
using FTStore.Infra.Repository;
using FTStore.Infra.Resource;
using FTStore.App.Repositories;
using FTStore.Infra.Context;
using Microsoft.EntityFrameworkCore;
using FTStore.App.Services;
using FTStore.App.Services.Impl;
using FTStore.App.Factories;
using FTStore.App.Factories.Impl;

namespace FTStore.Crosscutting.Helper
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }

        public static IServiceCollection AddFTStoreResources(this IServiceCollection services, string baseDir)
        {
            services.AddTransient<IProductFileManager>(gap => new ProductFileManager(baseDir));
            return services;
        }

        public static IServiceCollection AddAppDependencies(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddTransient<IProductFactory, ProductFactory>();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IUserFactory, UserFactory>();
            return services;
        }
    }
}
