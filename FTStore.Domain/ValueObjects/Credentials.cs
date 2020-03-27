using System.Text;
using FTStore.Domain.Common.ValueObjects;

namespace FTStore.Domain.ValueObjects
{
    public class Credentials : ValueObject<Credentials>
    {
        public string Email { get; private set; }
        public string Password { get; private set; }

        public Credentials(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public byte[] Hash()
        {
            return Encoding.UTF8.GetBytes(Email);
        }

        public byte[] Salt()
        {
            return Encoding.UTF8.GetBytes(Password);
        }

        protected override bool EqualsCore(Credentials other)
        {
            return this.Email.Equals(other.Email) &&
                this.Password.Equals(other.Password);
        }

        protected override int GetHashCodeCore()
        {
            return (GetType().GetHashCode() * 42) +
                this.Email.GetHashCode() + this.Password.GetHashCode();
        }
    }
}
