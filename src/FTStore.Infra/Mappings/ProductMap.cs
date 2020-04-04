using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FTStore.Infra.Table;

namespace FTStore.Infra.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<ProductTable>
    {
        public const int TITLE_SIZE = 200;
        public void Configure(EntityTypeBuilder<ProductTable> builder)
        {
            builder.ToTable("product");

            builder
                .HasKey(p => p.Id);
            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(TITLE_SIZE);
            builder
                .Property(p => p.Details)
                .IsRequired()
                .HasMaxLength(1000);
            builder
                .Property(p => p.Price)
                .HasColumnType("decimal(19,4)")
                .IsRequired();
        }
    }
}
