using FTStore.Domain.Common.Entities;

namespace FTStore.Domain.Entities
{
    public class CustomerEntity : Entity
    {
        public string Name { get; private set; }
        public override bool IsValid()
        {
            return Id > 0;
        }
    }
}
