using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
namespace FTStore.Infra.Tests.Fixture
{

    internal class HostEnvironmentFixture : IHostEnvironment
    {
        public string ApplicationName { get; set; }
        public IFileProvider ContentRootFileProvider { get; set; }
        public string ContentRootPath { get; set; }
        public string EnvironmentName { get; set; }
    }
}
