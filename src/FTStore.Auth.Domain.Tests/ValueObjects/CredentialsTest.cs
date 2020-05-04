using FluentAssertions;
using FTStore.Auth.Domain.ValueObjects;
using Xunit;

namespace FTStore.Auth.Domain.Tests.ValueObjects
{
    public class CredentialsTest
    {
        private const string SECRET_PHRASE = "swordfish";
        private const string EMAIL = "admin@admin.com";

        [Fact]
        public void ShouldCreateWithEMailAndSecretPhrase()
        {
            var credentials = new Credentials(EMAIL, SECRET_PHRASE);

            credentials.Should().NotBeNull();
            credentials.Email.Should().Be(EMAIL);
            credentials.Password.Should().NotBeNull();
        }

        [Fact]
        public void ShouldCreateWithEmailAndPassword()
        {
            var password = new Password(SECRET_PHRASE);

            var credentials = new Credentials(EMAIL, password);

            credentials.Email.Should().Be(EMAIL);
            credentials.Password.Should().Be(password);
        }

        [Fact]
        public void ShouldCredentialsBeEquals()
        {
            var credentials = new Credentials(EMAIL, SECRET_PHRASE);
            var hash = credentials.Password.Hash;
            var salt = credentials.Password.Salt;
            var password = new Password(hash, salt);
            var credentials2 = new Credentials(EMAIL, password);

            var credentialsEquals = credentials.Equals(credentials2);

            credentialsEquals.Should().BeTrue();
        }
    }
}
