using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FTStore.Domain.Entities;

namespace FTStore.Infra.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("Product");

            builder
                .HasKey(p => p.Id);
            builder
                .Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);
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
