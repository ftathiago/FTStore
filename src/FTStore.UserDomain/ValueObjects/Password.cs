using FTStore.Lib.Common.ValueObjects;
using FTStore.UserDomain.Libs;
using System.Linq;

namespace FTStore.UserDomain.ValueObjects
{
    public class Password : ValueObject<Password>
    {
        public byte[] Hash { get; private set; }
        public byte[] Salt { get; private set; }

        public Password(byte[] hash, byte[] salt)
        {
            Hash = hash;
            Salt = salt;
        }

        public Password(string password)
        {
            var hashSenha = new PasswordHashCalculator(password);
            Hash = hashSenha.PasswordHash;
            Salt = hashSenha.PasswordSalt;
        }

        protected override bool EqualsCore(Password other)
        {
            if (!Hash.SequenceEqual(other.Hash))
                return false;

            if (!Salt.SequenceEqual(other.Salt))
                return false;

            return true;
        }

        protected override int GetHashCodeCore()
        {
            return Hash.GetHashCode() + Salt.GetHashCode();
        }
    }
}
