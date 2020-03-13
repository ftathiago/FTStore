using Microsoft.EntityFrameworkCore;
using FTStore.Domain.Entity;
using FTStore.Domain.ValueObject;
using FTStore.Infra.Mappings;

namespace FTStore.Infra.Context
{
    public class FTStoreDbContext : DbContext
    {
        public DbSet<UserEntity> Usuarios { get; set; }
        public DbSet<ProductEntity> Produtos { get; set; }
        public DbSet<Order> Pedidos { get; set; }
        public DbSet<OrderItem> ItensPedido { get; set; }
        public DbSet<PaymentMethod> FormaPagamento { get; set; }

        public FTStoreDbContext(DbContextOptions options) : base(options)
        {
        }

        public FTStoreDbContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseMySql("server=127.0.0.1;uid=root;pwd=1701;database=FTStore");
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
    }
}