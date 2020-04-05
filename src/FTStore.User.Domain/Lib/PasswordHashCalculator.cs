using System;
using System.Security.Cryptography;
using System.Text;

namespace FTStore.User.Domain.Lib
{
    public class PasswordHashCalculator
    {
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }

        public PasswordHashCalculator(string password)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            CalcHash(password);
        }

        public PasswordHashCalculator(string password, byte[] key)
        {
            CalcHash(password, key);
        }

        private void CalcHash(string password, byte[] key = null)
        {
            using var hmac = new HMACSHA512();
            if (key != null)
                hmac.Key = key;
            var encodedString = Encoding.UTF8.GetBytes(password);
            PasswordSalt = hmac.Key;
            PasswordHash = hmac.ComputeHash(encodedString);
        }

    }
}
