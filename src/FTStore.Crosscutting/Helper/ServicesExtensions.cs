using System;

using Microsoft.Extensions.DependencyInjection;

using AutoMapper;

using FTStore.App.Factories;
using FTStore.App.Factories.Impl;
using FTStore.App.Repositories;
using FTStore.App.Services;
using FTStore.App.Services.Impl;
using FTStore.Domain.Repository;
using FTStore.Infra.Mappings.Profiles;
using FTStore.Infra.Repository;
using FTStore.Infra.Resource;


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

        public static IServiceCollection AddMapper(this IServiceCollection services, Type type)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<UserMapProfile>();
                cfg.AddProfile<ProductMapProfile>();
                cfg.AddProfile<OrderMapProfile>();
                cfg.AddProfile<CustomerMapProfile>();
                cfg.AddProfile<PaymentMethodMapProfile>();
            }, type);
            return services;
        }
    }
}
