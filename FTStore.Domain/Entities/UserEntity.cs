using System.Collections.Generic;

namespace FTStore.Domain.Entities
{
    public class UserEntity : Entity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public override void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}
