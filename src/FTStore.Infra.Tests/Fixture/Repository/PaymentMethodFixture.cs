using FTStore.Domain.Enum;
using FTStore.Domain.ValueObjects;
using FTStore.Infra.Table;

namespace FTStore.App.Tests.Fixture.Repository
{
    public static class PaymentMethodFixture
    {
        public const int ID = 1;
        public const string NAME = "Credit Card";
        public static PaymentMethodTable GetValid(int id = ID, string name = NAME)
        {
            var paymentMethod = new PaymentMethodTable();
            paymentMethod.Id = id;
            paymentMethod.Name = name;
            return paymentMethod;
        }

        public static PaymentMethod GetValidVO()
        {
            return new PaymentMethod(PaymentMethodsSupported.CreditCard);
        }
    }
}
