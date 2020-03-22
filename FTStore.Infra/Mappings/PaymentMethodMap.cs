using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FTStore.Domain.Enum;
using FTStore.Domain.ValueObjects;

namespace FTStore.Infra.Mappings
{
    public class PaymentMethodMap : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.ToTable("PaymentMehod");

            builder.HasKey(f => f.Id);
            builder
                .Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder
                .Property(f => f.Description)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasData(
                new PaymentMethod()
                {
                    Id = (int)PaymentMethodEnum.PaymentSlip,
                    Name = "Boleto",
                    Description = "Forma de pagamento Boleto"
                },
                new PaymentMethod()
                {
                    Id = (int)PaymentMethodEnum.CreditCard,
                    Name = "Cartão de Crédito",
                    Description = "Forma de pagamento Cartão de Crédito"
                },
                new PaymentMethod()
                {
                    Id = (int)PaymentMethodEnum.Deposit,
                    Name = "Depósito",
                    Description = "Forma de pagamento Depósito"
                }
            );
        }
    }
}
