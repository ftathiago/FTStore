using FTStore.Domain.Enum;

namespace FTStore.Domain.ValueObjects
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsPaymentSlip
        {
            get { return Id == (int)PaymentMethodEnum.PaymentSlip; }
        }

        public bool IsCreditCard
        {
            get { return Id == (int)PaymentMethodEnum.CreditCard; }
        }

        public bool IsDeposit
        {
            get { return Id == (int)PaymentMethodEnum.Deposit; }
        }

        public bool IsUnknow
        {
            get { return Id == (int)PaymentMethodEnum.Unknow; }
        }
    }
}
