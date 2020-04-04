using FTStore.Domain.Entities;
using FTStore.Infra.Table;

namespace FTStore.App.Tests.Fixture.Repository
{
    public class CustomerFixture
    {
        public const int ID = 1;
        public const string NAME = "Customer name";
        public const string SURNAME = "Surname";
        public CustomerTable GetValid(int id = ID)
        {
            var customer = new CustomerTable();
            customer.Id = id;
            customer.Name = NAME;
            customer.Surname = SURNAME;

            return customer;
        }

        public CustomerEntity GetValidEntity(int id = ID)
        {
            var customer = new CustomerEntity(NAME, SURNAME);
            customer.DefineId(id);
            return customer;
        }
    }
}
