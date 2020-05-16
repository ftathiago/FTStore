using System;
using FTStore.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FTStore.Crosscutting.Model;
using MySql.Data.MySqlClient;

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

        public static string BuildConnectionString(this IConfiguration configuration)
        {
            var appSettings = GetAppSettings(configuration);
            var builder = new MySqlConnectionStringBuilder(configuration.GetConnectionString("FTStoreDB"));
            builder.Database = "FTStore";
            if (!string.IsNullOrEmpty(appSettings.DBHost))
                builder.Server = appSettings.DBHost;
            if (!string.IsNullOrWhiteSpace(appSettings.DBUsuario))
                builder.UserID = appSettings.DBUsuario;
            if (!string.IsNullOrWhiteSpace(appSettings.DBSenha))
                builder.Password = appSettings.DBSenha;
            var connectionString = builder.ConnectionString;
            return connectionString;
        }

        private static AppSettings GetAppSettings(IConfiguration configuration)
        {
            var appSettings = new AppSettings();
            appSettings.DBHost = configuration["DBHOST"] ?? configuration["DatabaseConfig:DatabaseHost"];
            appSettings.DBUsuario = configuration["USERID"] ?? configuration["DatabaseConfig:UserID"];
            appSettings.DBSenha = configuration["USERPASS"] ?? configuration["DatabaseConfig:UserPass"];
            return appSettings;
        }

        public static IHost InitializeDataBase(this IHost host)
        {
            var serviceScopeFactory = host.Services;

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;


                FTStoreDbContext dbContext = services.GetRequiredService<FTStoreDbContext>();
                dbContext.Database.Migrate();
            }

            return host;
        }
    }
}
