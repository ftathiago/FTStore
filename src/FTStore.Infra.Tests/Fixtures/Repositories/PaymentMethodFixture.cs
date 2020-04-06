using FTStore.Domain.Enums;
using FTStore.Domain.ValueObjects;
using FTStore.Infra.Tables;

namespace FTStore.Infra.Tests.Fixtures.Repositories
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
