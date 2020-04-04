using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FTStore.Domain.Enum;
using FTStore.Infra.Table;

namespace FTStore.Infra.Mappings
{
    public class PaymentMethodMap : IEntityTypeConfiguration<PaymentMethodTable>
    {
        public void Configure(EntityTypeBuilder<PaymentMethodTable> builder)
        {
            builder.ToTable("paymentmethod");

            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
                new PaymentMethodTable()
                {
                    Id = (int)PaymentMethodEnum.PaymentSlip,
                    Name = "Boleto",
                    Description = "Forma de pagamento Boleto"
                },
                new PaymentMethodTable()
                {
                    Id = (int)PaymentMethodEnum.CreditCard,
                    Name = "Cartão de Crédito",
                    Description = "Forma de pagamento Cartão de Crédito"
                },
                new PaymentMethodTable()
                {
                    Id = (int)PaymentMethodEnum.Deposit,
                    Name = "Depósito",
                    Description = "Forma de pagamento Depósito"
                }
            );
        }
    }
}
