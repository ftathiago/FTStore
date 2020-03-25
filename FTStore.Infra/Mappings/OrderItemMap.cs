using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FTStore.Domain.Entities;
using FTStore.Infra.Model;

namespace FTStore.Infra.Mappings
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItemModel>
    {
        public void Configure(EntityTypeBuilder<OrderItemModel> builder)
        {
            builder.ToTable("OrderItem");

            builder.HasKey(i => i.Id);
            builder
                .Property(i => i.ProductId)
                .IsRequired();
            builder
                .Property(i => i.Title)
                .HasMaxLength(ProductMap.TITLE_SIZE);
            builder
                .Property(i => i.Price)
                .IsRequired();
            builder
                .Property(i => i.Quantity)
                .IsRequired();
        }
    }
}
