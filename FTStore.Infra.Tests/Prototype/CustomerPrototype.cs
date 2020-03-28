using FTStore.Domain.Entities;
using FTStore.Infra.Model;

namespace FTStore.Infra.Tests.Prototype
{
    public class CustomerPrototype
    {
        public const int ID = 1;
        public const string NAME = "Customer name";
        public const string SURNAME = "Surname";
        public CustomerModel GetValid(int id = ID)
        {
            var customer = new CustomerModel();
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
