using System;
using FTStore.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace FTStore.Auth.Infra.Test.Fixtures
{
    public class ContextFixture
    {
        public FTStoreAuthContext CreateNewDbContext()
        {
            var options = new DbContextOptionsBuilder<FTStoreAuthContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            return new FTStoreAuthContext(options);
        }
    }

}