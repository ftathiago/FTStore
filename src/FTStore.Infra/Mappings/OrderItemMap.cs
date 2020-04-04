using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FTStore.Infra.Table;

namespace FTStore.Infra.Mappings
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItemTable>
    {
        public void Configure(EntityTypeBuilder<OrderItemTable> builder)
        {
            builder.ToTable("orderitem");

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
