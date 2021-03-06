using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FTStore.Infra.Tables;

namespace FTStore.Infra.Mappings
{
    public class OrderMap : IEntityTypeConfiguration<OrderTable>
    {
        public void Configure(EntityTypeBuilder<OrderTable> builder)
        {
            builder.ToTable("order");

            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.OrderDate)
                .IsRequired();

            builder
                .Property(p => p.CustomerId)
                .IsRequired();

            builder
                .Property(p => p.DeliveryForecast)
                .IsRequired();

            builder
                .Property(p => p.Street)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .Property(p => p.AddressNumber)
                .IsRequired()
                .HasMaxLength(4);

            builder
                .Property(p => p.Neighborhood)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(p => p.City)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(p => p.State)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(p => p.ZIPCode)
                .IsRequired()
                .HasMaxLength(10);

            builder
                .Property(p => p.PaymentMethodId)
                .IsRequired();
        }
    }
}
