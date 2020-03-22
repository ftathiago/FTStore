using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FTStore.Domain.Entity;

namespace FTStore.Infra.Mappings
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem");

            builder.HasKey(i => i.Id);
            builder
                .Property(i => i.ProductId)
                .IsRequired();
            builder
                .Property(i => i.Quantity)
                .IsRequired();
        }
    }
}