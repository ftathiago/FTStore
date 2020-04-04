using FTStore.Domain.Enum;

namespace FTStore.Domain.ValueObjects
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        protected PaymentMethod() { }
        public PaymentMethod(PaymentMethodsSupported paymentMethod)
        {
            this.Id = (int)paymentMethod;
        }

        public bool IsPaymentSlip
        {
            get { return Id == (int)PaymentMethodsSupported.PaymentSlip; }
        }

        public bool IsCreditCard
        {
            get { return Id == (int)PaymentMethodsSupported.CreditCard; }
        }

        public bool IsDeposit
        {
            get { return Id == (int)PaymentMethodsSupported.Deposit; }
        }

        public bool IsUnknow
        {
            get { return Id == (int)PaymentMethodsSupported.Unknow; }
        }
    }
}
