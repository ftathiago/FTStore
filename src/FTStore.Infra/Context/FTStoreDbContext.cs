using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

        private readonly IHostEnvironment _env;

        public FTStoreDbContext() : base()
        { }

        public FTStoreDbContext(DbContextOptions options) : base(options)
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
        private static readonly ILoggerFactory DbLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder //NOSONAR
                .AddFilter((category, level) =>
                    category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information)
                .AddConsole();
        });
    }
}
