using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FTStore.Domain.Entities;

namespace FTStore.Infra.Mappings
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(p => p.Id);
            builder
                .Property(p => p.OrderDate)
                .IsRequired();
            builder
                .Property(p => p.DeliveryForecast)
                .IsRequired();
            builder
                .Property(p => p.ZipCode)
                .IsRequired()
                .HasMaxLength(10);
            builder
                .Property(p => p.City)
                .IsRequired()
                .HasMaxLength(100);
            builder
                .Property(p => p.State)
                .IsRequired()
                .HasMaxLength(100);
            builder
                .Property(p => p.FullAddress)
                .IsRequired()
                .HasMaxLength(100);
            builder
                .Property(p => p.AddressNumber)
                .IsRequired();
        }
    }
}
