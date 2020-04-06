using FTStore.Infra.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTStore.Infra.Mappings
{
    public class CustomerMap : IEntityTypeConfiguration<CustomerTable>
    {
        public void Configure(EntityTypeBuilder<CustomerTable> builder)
        {
            builder
                .ToTable("customer");

            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(c => c.Surname)
                .HasMaxLength(250)
                .IsRequired();
        }
    }
}
