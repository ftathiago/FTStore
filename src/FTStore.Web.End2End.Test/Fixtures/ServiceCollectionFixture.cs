using System;

using Microsoft.Extensions.DependencyInjection;

using FTStore.App.Repositories;
using FTStore.Crosscutting.Helpers;

using Moq;

namespace FTStore.Web.End2End.Test.Fixtures
{
    public class ServiceCollectionFixture
    {
        public ServiceCollectionFixture()
        {

        }

        public IServiceProvider ServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();
            services = MockFileManager(services);
            services
                .AddMapper(typeof(ServicesExtensions))
                .AddRepositories()
                .AddAppDependencies()
                .AddMemoryDataBase();
            return services.BuildServiceProvider();
        }

        private IServiceCollection MockFileManager(IServiceCollection services)
        {
            var productFileManager = new Mock<IProductFileManager>(MockBehavior.Strict);
            services.AddTransient<IProductFileManager>(gap => productFileManager.Object);
            return services;
        }
    }
}
