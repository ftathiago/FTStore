using System.Collections.Generic;

using FTStore.Lib.Common.Entities;

namespace FTStore.Domain.Entities
{
    public class User : Entity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}
