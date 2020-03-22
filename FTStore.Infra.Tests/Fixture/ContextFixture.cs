using System;
using FTStore.Infra.Context;
using Microsoft.EntityFrameworkCore;


namespace FTStore.Infra.Tests.Fixture
{
    public class ContextFixture : IDisposable
    {
        private bool _disposed;

        public FTStoreDbContext Ctx => FakeFTStoreDbContext();

        private static FTStoreDbContext FakeFTStoreDbContext()
        {
            var options = new DbContextOptionsBuilder<FTStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            return new FTStoreDbContext(options, new HostEnvironmentFixture());
        }

        ~ContextFixture()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool dispose)
        {
            if (_disposed) return;

            if (dispose)
            {
                Ctx?.Dispose();
            }

            _disposed = true;
        }
    }

}

