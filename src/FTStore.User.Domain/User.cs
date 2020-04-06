using FTStore.Domain.Common.Entities;
using FTStore.User.Domain.ValueObjects;

namespace FTStore.User.Domain
{
    public class User : Entity
    {
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public string EMail { get; protected set; }
        public Password Password { get; protected set; }

        public User(string name, string surname, string email, Password password)
        {
            Name = name;
            Surname = surname;
            email = EMail;
            Password = new Password(password.Hash, password.Salt);
        }

        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}
