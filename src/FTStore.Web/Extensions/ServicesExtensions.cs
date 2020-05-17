using FTStore.Web.Services;
using FTStore.Web.Services.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace FTStore.Web.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureWebApiServices(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();
            return services;
        }
    }
}