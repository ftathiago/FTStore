using FTStore.Lib.Common.Entities;

namespace FTStore.Domain.Entities
{
    public class Customer : Entity
    {
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public Customer(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public override bool IsValid()
        {
            return Id > 0;
        }
    }
}
