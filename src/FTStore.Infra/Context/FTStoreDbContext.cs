using Microsoft.EntityFrameworkCore;

using FTStore.Infra.Mappings;
using FTStore.Infra.Tables;

namespace FTStore.Infra.Context
{
    public class FTStoreDbContext : DbContext
    {

        public DbSet<ProductTable> Products { get; set; }
        public DbSet<OrderTable> Orders { get; set; }
        public DbSet<PaymentMethodTable> PaymentMethod { get; set; }
        public DbSet<CustomerTable> Customers { get; set; }

        public FTStoreDbContext() : base()
        { }

        public FTStoreDbContext(DbContextOptions<FTStoreDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderItemMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new PaymentMethodMap());
            modelBuilder.ApplyConfiguration(new CustomerMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
