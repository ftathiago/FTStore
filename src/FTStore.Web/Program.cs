using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using FTStore.Crosscutting.Helpers;

namespace FTStore.Web
{
    public class Program
    {
        public static void Main(string[] args)//NOSONAR
        {
            CreateHostBuilder(args)
                .Build()
                .InitializeDataBase()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
