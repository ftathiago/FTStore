using Microsoft.EntityFrameworkCore;
using FTStore.Auth.Infra.Mappings;
using FTStore.Auth.Infra.Tables;

namespace FTStore.Infra.Context
{
    public class FTStoreAuthContext : DbContext
    {
        public DbSet<UserTable> Users { get; set; }

        public FTStoreAuthContext() : base()
        { }

        public FTStoreAuthContext(DbContextOptions<FTStoreAuthContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
