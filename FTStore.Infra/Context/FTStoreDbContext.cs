using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FTStore.Domain.ValueObjects;
using FTStore.Infra.Mappings;
using FTStore.Infra.Model;

namespace FTStore.Infra.Context
{
    public class FTStoreDbContext : DbContext
    {

        public DbSet<ProductModel> Produtos { get; set; }
        public DbSet<OrderModel> Pedidos { get; set; }
        public DbSet<OrderItemModel> ItensPedido { get; set; }
        public DbSet<PaymentMethodModel> PaymentMethod { get; set; }

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

            base.OnModelCreating(modelBuilder);
        }
        private static readonly ILoggerFactory DbLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter((category, level) =>
                    category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information)
                .AddConsole();
        });
    }
}
