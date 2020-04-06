using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FTStore.Web
{
    public class Program
    {
        public static void Main(string[] args)//NOSONAR
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
