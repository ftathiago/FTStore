using System.Collections.Generic;

namespace FTStore.Domain.Entities
{
    public class UserEntity : FTStore.Domain.Common.Entities.Entity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<OrderEntity> Orders { get; set; }

        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}
