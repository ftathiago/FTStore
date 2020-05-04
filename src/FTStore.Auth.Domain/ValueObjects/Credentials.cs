using FTStore.Lib.Common.ValueObjects;

namespace FTStore.Auth.Domain.ValueObjects
{
    public class Credentials : ValueObject<Credentials>
    {
        public string Email { get; private set; }
        public Password Password { get; private set; }

        public Credentials(string email, string password)
        {
            Email = email;
            Password = new Password(password);
        }

        public Credentials(string email, Password password)
        {
            Email = email;
            Password = password;
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
