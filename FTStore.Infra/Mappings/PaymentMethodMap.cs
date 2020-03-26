using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FTStore.Domain.Enum;
using FTStore.Infra.Model;

namespace FTStore.Infra.Mappings
{
    public class PaymentMethodMap : IEntityTypeConfiguration<PaymentMethodModel>
    {
        public void Configure(EntityTypeBuilder<PaymentMethodModel> builder)
        {
            builder.ToTable("PaymentMehod");

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
                new PaymentMethodModel()
                {
                    Id = (int)PaymentMethodEnum.PaymentSlip,
                    Name = "Boleto",
                    Description = "Forma de pagamento Boleto"
                },
                new PaymentMethodModel()
                {
                    Id = (int)PaymentMethodEnum.CreditCard,
                    Name = "Cartão de Crédito",
                    Description = "Forma de pagamento Cartão de Crédito"
                },
                new PaymentMethodModel()
                {
                    Id = (int)PaymentMethodEnum.Deposit,
                    Name = "Depósito",
                    Description = "Forma de pagamento Depósito"
                }
            );
        }
    }
}
