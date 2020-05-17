using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using FTStore.Crosscutting.Helpers;
using Microsoft.Extensions.Configuration;

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
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    var env = hostContext.HostingEnvironment;

                    config.Sources.Clear();

                    config.AddEnvironmentVariables(prefix: "FTSTORE_");
                    config
                        .AddJsonFile(
                            "config.json",
                            optional: false,
                            reloadOnChange: true)
                        .AddJsonFile(
                            $"config.{env.EnvironmentName}.json",
                            optional: true,
                            reloadOnChange: true);

                    config.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
