using FTStore.Domain.Enum;
using FTStore.Domain.ValueObjects;
using FTStore.Infra.Model;

namespace FTStore.Infra.Tests.Prototype
{
    public static class PaymentMethodPrototype
    {
        public const int ID = 1;
        public const string NAME = "Credit Card";
        public static PaymentMethodModel GetValid(int id = ID, string name = NAME)
        {
            var paymentMethod = new PaymentMethodModel();
            paymentMethod.Id = id;
            paymentMethod.Name = name;
            return paymentMethod;
        }

        public static PaymentMethod GetValidVO()
        {
            return new PaymentMethod(PaymentMethodEnum.CreditCard);
        }
    }
}
