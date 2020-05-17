using System.Collections.Generic;
using FTStore.Lib.Common.Entities;
using FTStore.Auth.Domain.ValueObjects;

namespace FTStore.Auth.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public string Email { get; protected set; }
        public Password Password { get; protected set; }

        public ICollection<string> Claims { get; protected set; }

        public User()
        {
            Claims = new List<string>();
        }
        public User(string name, string surname, string email, Password password) : this()
        {
            Name = name;
            Surname = surname;
            Email = email;
            Password = new Password(password.Hash, password.Salt);
        }

        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }

        public bool IsValidCredentials(Credentials credentials)
        {
            var userCredentials = new Credentials(this.Email, this.Password);
            return userCredentials.Equals(credentials);
        }
    }
}
