using FTStore.Domain.Common.Entities;

namespace FTStore.Domain.Entities
{
    public class CustomerEntity : Entity
    {
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public CustomerEntity(string name, string surname)
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
