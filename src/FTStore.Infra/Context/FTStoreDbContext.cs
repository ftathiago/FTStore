using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FTStore.Infra.Mappings;
using FTStore.Infra.Table;

namespace FTStore.Infra.Context
{
    public class FTStoreDbContext : DbContext
    {

        public DbSet<ProductTable> Products { get; set; }
        public DbSet<OrderTable> Orders { get; set; }
        public DbSet<PaymentMethodTable> PaymentMethod { get; set; }
        public DbSet<CustomerTable> Customers { get; set; }
        public DbSet<UserTable> Users { get; set; }

        private readonly IHostEnvironment _env;

        public FTStoreDbContext() : base()
        { }

        public FTStoreDbContext(DbContextOptions options) : base(options)
        { }

        public FTStoreDbContext(DbContextOptions<FTStoreDbContext> opt, IHostEnvironment env)
            : base(opt)
        {
            _env = env;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_env != null && _env.IsDevelopment())
            {
                optionsBuilder.UseLoggerFactory(DbLoggerFactory);
            }

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                   .UseLazyLoadingProxies()
                   .UseMySql("server=127.0.0.1;uid=root;pwd=1701;database=FTStore");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
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
